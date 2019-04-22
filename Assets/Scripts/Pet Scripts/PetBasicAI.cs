using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PetBasicAI : MonoBehaviour
{
    #region Declaração das Variáveis
    [Tooltip("Tempo entre as verificações de ações do Pet (em segundos)")]
    public float timeBetweenAction = 1;

    [Tooltip("Valores limitantes para os atributos do pet")]
    public Health healthLimit;
    [Tooltip("Valor para aviso de fome")]
    public float hungryWarning;
    [Tooltip("Valor para aviso de sede")]
    public float thirstyWarning;

    public static PetBasicAI instance; // Garantir a unicidade

    protected Health petHealth; // Para facilitar o acesso a informação
    protected int randomNumber; // Número aleatório utilizado na movimetnação alatória do pet

    protected float randomActionCountdown = 0;
    [Tooltip("Quantidade de vezes que o pet verifica que não há nada mais importante para fazer antes de fazer algum movimento aleatório")]
    public int maxIdleTime = 3;

    [Tooltip("Range de liberdade de movimento do pet (min = esquerda)")]
    public float moveRangeMin = -20;
    [Tooltip("Range de liberdade de movimento do pet (max = direita)")]
    public float moveRangeMax = 20;
    [Tooltip("Multiplicador para o range do movimento")]
    public float moveRangeMultiplier = 50;

    // Nome do script com as animações do pet - para poder acessar as animações
    private PetMovement petAnimationScript;

    // Valores minimo e máximo para o tempo aleatório de verificação das necessidades do pet
    readonly private float actionMinRandom = 1;
    readonly private float actionMaxRandom = 5;
    // Variável que armazena o valor aleatório gerado
    private float actionRandom;

    // DELEGATE: variável que pode armazenar funções
    // Utilizada para armazenar as funções correspondentes as ações que o pet quer realizar
    // As funções serão chamadas através do inicializador da DELEGATE, de modo genérico
    public delegate IEnumerator PetAction();
    private PetAction petActionList = null;
    // Vetor de delegates (utilizado para chamar uma delegate de cada vez no pacote de delegates)
    private System.Delegate[] petDelegateList;

    //#region Variáveis utilizadas para bloquear uma ação que já está armazenada na delegate e ainda não foi chamada
    private bool hungryOnDelegate = false;
    //private bool hungryWarningOnDelegate = false;
    private bool thirstyOnDelegate = false;
    //private bool thirstyWarningOnDelegate = false;
    //private bool sadOnDelegate = false;
    //private bool peeOnDelegate = false;
    //private bool poopOnDelegate = false;
    //#endregion

    #region Variáveis para o grafo de locais de acesso do pet
    [SerializeField]
    public GraphCreator[] petAccessInfo;
    private int petAccessInfoIndex;
    [SerializeField]
    private Graph<string> petAccessGraph;
    #endregion

    // Variável que indica se o pet já está realizando alguma ação (não permite que várias ações sejam execurtadas em paralelo)
    public bool isPetDoingSomething = false;
    private bool waitForActionEnd;

    private Pet pet;
    private float chanceToGoToActiveScene = 0.5f;
    private float chanceToBeThirsty = 0f;
    [SerializeField]
    private float increaseChanceToBeThirsty;
    private int drinkCount = 0;

    #region Para testes
    private Food food;
    public Player player;
    #endregion

    #endregion

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (this != instance)
        {
            Destroy(gameObject);
        }
        // Para o pet poder continuar a realizar suas ações mesmo quando o jogador não estiver na cena com o pet
        DontDestroyOnLoad(gameObject);

        pet = gameObject.GetComponentInChildren<Pet>();

        #region Para testes
        player = SaveManager.instance.player;
        pet.SetPetLocation(new ElementLocation("Pet", SceneManager.GetActiveScene().name, gameObject.transform.position));
        player.foodPotLocation = new ElementLocation("Pote de Comida", "MainRoom", new Vector3(1500, -410, 5));
        player.waterPotLocation = new ElementLocation("Pote de Água", "Yard(1)", new Vector3(-300, -356, 5));
        player.wasteLocation = new ElementLocation("Jornal", "Yard(1)", new Vector3(500, -356, 0));
        player.foodPot = new PotStatus(new Food("Ração", 1f));
        player.waterPot = new PotStatus(new Food("Água", 1f));
        #endregion

        petAnimationScript = gameObject.GetComponentInChildren<PetMovement>();
        petHealth = SaveManager.instance.player.health;

        petAccessInfoIndex = PetAccessListSelection();
        petAccessGraph = petAccessInfo[petAccessInfoIndex].CreateGraph();

        IncreaseChanceCalculate();

        StartCoroutine(PetActionVerifier());
    }

    /// <summary>
    /// Função que verifica que ação o pet irá realizar baseado no estado atual do pet e no que ele já realizou anteriormente
    /// </summary>
    /// <returns></returns>
    private IEnumerator PetActionVerifier()
    {
        // Número aleatório gerado para definir quando ocorrerá a próxima verificação
        actionRandom = Random.Range(actionMinRandom, actionMaxRandom);
        if (!isPetDoingSomething)
        {
            // VERIFICAÇÃO DO ESTADO DO PET
            // Verifica se um status específico está fora do limite aceitável para o pet e se a ação já está na lista
            // Se estiver fora do limite e não estiver na lista de ações, adiciona na lista (delegate) e sinaliza como adicionado
            if (petHealth.GetHungry() && !hungryOnDelegate)
            {
                petActionList += PetHungry;
                hungryOnDelegate = true;
            }
            if (drinkCount < pet.drinkTimes[TimeManager.instance.GetCurrentPeriod()] && !thirstyOnDelegate)
            {
                if (Random.Range(0f, 1f) <= chanceToBeThirsty)
                {
                    Debug.LogWarning("bebe água " + chanceToBeThirsty);
                    drinkCount++;
                    petActionList += PetThisty;
                    thirstyOnDelegate = true;
                }
                else
                {
                    chanceToBeThirsty += increaseChanceToBeThirsty;
                }
            }
            /*
            if (petHealth.GetHappiness() < healthLimit.GetHappiness() && !sadOnDelegate)
            {
                petActionList += PetSad;
                sadOnDelegate = true;
            }*/

            // Se a lista de ações do pet não estiver vazia, realiza a primeira ação da lista e a remove da lista
            if (petActionList != null)
            {
                // Discretiza a lista de ações da delegate em um vetor de delegates
                petDelegateList = petActionList.GetInvocationList();
                // Chama a função que está armazenada na primeira posição do vetor de delegates como uma coroutine
                StartCoroutine(petDelegateList[0].Method.Name);
                // Remove esta função da lista de funções da delegate
                petActionList -= petDelegateList[0] as PetAction;
            }
            // Caso contrário, chama a função que realiza movimentos aleatórios
            else
            {
                StartCoroutine(PetRandomMove());
            }
        }

        // Aguarda o tempo aleatório gerado anteriormente
        yield return new WaitForSeconds(actionRandom);

        // Chama a mesma função (para uma nova verificação do estado do pet)
        StartCoroutine(PetActionVerifier());
    }
    
    #region Funções específicas de controle de cada ação do PET
    /// <summary>
    /// Função que comanda o pet quando ele está com fome
    /// </summary>
    public IEnumerator PetHungry()
    {
        isPetDoingSomething = true;
        Debug.Log("Fome");

        StartCoroutine(MoveToPosition(player.foodPotLocation, IsPetDoingSometingSetFalse));
        yield return new WaitForEndOfFrame();
    }
    
    public IEnumerator PetGoEat()
    {
        yield return new WaitUntil(() => !isPetDoingSomething);

        isPetDoingSomething = true;
        Debug.Log("Foi comer");

        StartCoroutine(MoveToPosition(player.foodPotLocation, pet.Eat));
        yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// Função que comanda o pet quando ele está com sede
    /// </summary>
    private IEnumerator PetThisty()
    {
        isPetDoingSomething = true;
        Debug.Log("Sede");

        StartCoroutine(MoveToPosition(player.waterPotLocation, pet.Drink));
        yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// Função que controla o pet quando ele está com vontade de fazer xixi
    /// </summary>
    private IEnumerator PetPee()
    {
        waitForActionEnd = true;

        // Verifica se o pet sabe (foi ensinado) o local correto de fazer as suas necessidades
        if (Random.Range(0f, 1f) <= player.health.GetWhereToPP())
        {
            // Se ele sabe o local, se move até lá e faz
            Debug.Log("move");
            StartCoroutine(MoveToPosition(player.wasteLocation, pet.PeeOnLocation));
        }
        else
        {
            // Caso contrário, faz no local em que está
            StartCoroutine(pet.PeeOnLocation());
        }
        Debug.Log("Pee");
        waitForActionEnd = false;
        yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// Função que controla o pet quando ele está com vontade de fazer coco
    /// </summary>
    private IEnumerator PetPoop()
    {
        waitForActionEnd = true;

        // Verifica se o pet sabe (foi ensinado) o local correto de fazer as suas necessidades
        if (Random.Range(0f, 1f) <= player.health.GetWhereToPP())
        {
            // Se ele sabe o local, se move até lá e faz
            Debug.Log("move");
            StartCoroutine(MoveToPosition(player.wasteLocation, pet.PoopOnLocation));
        }
        else
        {
            // Caso contrário, faz no local em que está
            StartCoroutine(pet.PoopOnLocation());
        }
        Debug.Log("Poop");
        waitForActionEnd = false;
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator CallPeePoop()
    {
        StartCoroutine(PetPee());
        yield return new WaitWhile(() => waitForActionEnd);
        StartCoroutine(PetPoop());
        yield return new WaitWhile(() => waitForActionEnd);
    }

    /// <summary>
    /// Função que controla os movimentos aleatórios do pet
    /// Movimentos que ocorrem quando o pet não possui nenhuma outra necessidade
    /// </summary>
    public IEnumerator PetRandomMove()
    {
        // Verifica se o limite de vezes que a função deve ser chamada foi atingido
        if (randomActionCountdown >= maxIdleTime)
        {
            isPetDoingSomething = true;
            // Pet verifica se está na ActiveScene
            if (pet.petCurrentLocation.sceneName != SceneManager.GetActiveScene().name)
            {
                // Se não estiver, há uma chance de o pet ir para a ActiveScene
                if (Random.Range(0f, 1f) <= chanceToGoToActiveScene)
                {
                    Debug.Log("Indo pra active scene");
                    // Busca a rota para a active scene a partir da localização atual do pet
                    var path = petAccessGraph.BFS(pet.petCurrentLocation.sceneName, SceneManager.GetActiveScene().name);
                    // Concatena a resposta em uma string, separando os nomes das scenes por vírgulas
                    // Depois separa a string em um vetor de strings, separando pelas vírgulas
                    string[] name = HasHSetToString(path).Split(',');
                    // Para cada elemento do vetor, realiza os movimentos do pet
                    for (int i = name.Length - 1; i > 0; i--)
                    {
                        //float movePosition = petAccessInfo[petAccessInfoIndex].petAccessGraph.GetGraphCost(name[i], name[i - 1]);
                        // Pega a posição da próxima porta para onde o pet irá
                        float newPosition = petAccessInfo[petAccessInfoIndex].petAccessGraph.GetGraphCost(name[i - 1], name[i]);
                        // Pega a posição atual do pet para posicioná-lo na posição exata
                        Vector3 petPosition = pet.gameObject.transform.position;
                        // Coloca o pet na posição x da porta, enquanto mantem as posições y e z do pet
                        pet.gameObject.transform.position = new Vector3(newPosition, petPosition.y, petPosition.z);
                        // Informa que o pet mudou de scene
                        StartCoroutine(gameObject.GetComponent<Invisible>().PetChangeLocation(name[i - 1]));
                        // Pega a posição central da camera (localização para onde o pet irá)
                        Vector3 midScreen = new Vector3(); //petPosition;
                        midScreen = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 1f));
                        // Chama a animação de movimento do pet
                        petAnimationScript.MoveAnimalAndando(midScreen.x);
                        // Aguarda o término da animação
                        yield return new WaitUntil(() => !petAnimationScript.isWalking);
                    }
                    // Retorna a chance para 50%
                    chanceToGoToActiveScene = 0.5f;
                } 
                else
                {
                    // Caso o pet não foi para a ActiveScene, aumenta a chace em 5%
                    chanceToGoToActiveScene += 0.05f;
                }
            }
            // Se o pet estiver na ActiveScene, verifica se o pet vai realizar alguma ação aleatória
            else
            {
                randomActionCountdown = 0;
                // Escolhe um valor aleatório para selecionar qual ação o pet irá realizar
                randomNumber = Random.Range(0, 2);
                switch (randomNumber)
                {
                    case 0: // Pet se coça
                        Debug.Log("Movimento aleatório - Coçando");
                        // Chama a animação de coçar e aguarda
                        petAnimationScript.Cocando();
                        yield return new WaitForSeconds(1);
                        break;
                    case 1: // Pet se move até um ponto aleatório na scene
                        float move = Random.Range(moveRangeMin, moveRangeMax) * moveRangeMultiplier;
                        Debug.Log("Movimento aleatório - Andando " + move);
                        // Chama a animação de mover e aguarda o fim do movimento
                        petAnimationScript.MoveAnimalAndando(move);
                        yield return new WaitUntil(() => !petAnimationScript.isWalking);
                        break;
                }
            }
            isPetDoingSomething = false;
        }
        else // Caso contrário, incrementa o contador
        {
            // Se o pet não está feliz, o contador cresce mais lentamente
            if (petHealth.GetHappiness() < 0.5f)
            {
                randomActionCountdown += 0.5f;
            }
            else
            {
                randomActionCountdown++;
            }
        }
    }

    #endregion

    #region Funções auxiliares
    /// <summary>
    /// Calcula a chance de algo acontecer, em porcentagem
    /// </summary>
    /// <param name="min">Limite inferior do atributo</param>
    /// <param name="max">Limite superior do atributo</param>
    /// <param name="value">Valor do atributo para verificar a chance</param>
    /// <returns>Retorna a chance em porcentagem, entre 0 e 1</returns>
    private float ChanceToHappen(float min, float max, float value)
    {
        return Mathf.Clamp01((max - value) / (max - min));
    }

    /// <summary>
    /// Transforma uma HashSet em uma string separada por virgulas
    /// </summary>
    /// <param name="hashSet"></param>
    /// <returns></returns>
    private string HasHSetToString(HashSet<string> hashSet)
    {
        return string.Join(",", hashSet);
    }

    /// <summary>
    /// EM CONSTRUÇÃO
    /// Função que retorna o indice de qual grafo de acesso deve ser utilizado
    /// </summary>
    /// <returns></returns>
    public int PetAccessListSelection()
    {
        return 0;
    }

    /// <summary>
    /// Para todas as corrotinas neste script
    /// </summary>
    public void StoptPetCoroutines()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// Função que informa ao Pet que a ação que o mesmo estava realizando terminou
    /// </summary>
    /// <returns></returns>
    public IEnumerator IsPetDoingSometingSetFalse()
    {
        Debug.Log("Pet não está fazendo nada");
        isPetDoingSomething = false;
        yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// Função que movimenta o pet para um local determinado e chama uma função quando chegar lá
    /// </summary>
    /// <param name="element"> Local para onde o pet deve ir (variável do tipo ElementLocation) </param>
    /// <param name="functionToCall"> Função a ser chamada (do tipo IEnumerator sem nenhum parametro de entrada) </param>
    /// <returns></returns>
    public IEnumerator MoveToPosition(ElementLocation element, PetAction functionToCall)
    {
        // Chama a verificação do status do pet (verifica se ele está na scene ou não e "instancia" caso necessário)
        gameObject.GetComponent<Invisible>().StatusVerify();

        // Nome da scene em que o pet se encontra
        string currentScene = pet.GetPetLocation().sceneName;
        float movePosition;
        float newStartPosition = 0;
        string temp_name = currentScene;
        // Vaariável que indica se o pet deve continuar procurando pelo local ou se ele já se encontra lá
        bool keepSearching = false;

        // Se o pet está na scene que ele quer estar
        if (currentScene == element.sceneName)
        {
            // O pet se move para o local do objeto que ele estava procurando
            movePosition = element.elementPosition.x;
        }
        else
        {
            // Pet busca pelo caminho que deve seguir até a scene que ele quer estar (scene em que está o objeto que ele está procurando)
            var path = petAccessGraph.BFS(currentScene, element.sceneName);
            // Transforma a informação em uma string separada por vírgulas
            // Depois separa em um vetor de strings, separando pelas vírgulas
            string[] name = HasHSetToString(path).Split(',');

            // Pega o nome da penúltima scene na lista de scenes
            temp_name = name[name.Length - 2];
            // Informa que ele deve continuar procurando pois ainda não está na scene correta
            keepSearching = true;
            // Pega  aposição da porta por qual o pet deve entrar para a scene que ele quer ir
            movePosition = petAccessInfo[petAccessInfoIndex].petAccessGraph.GetGraphCost(name[name.Length - 1], name[name.Length - 2]);
            // Pega a posição da porta por qual ele irá sair na nova scene
            newStartPosition = petAccessInfo[petAccessInfoIndex].petAccessGraph.GetGraphCost(name[name.Length - 2], name[name.Length - 1]);
        }

        // Faz o pet se mover até a posição
        petAnimationScript.MoveAnimalAndando(movePosition);
        
        // Aguarda o fim da movimentação para continuar
        yield return new WaitUntil(() => !petAnimationScript.isWalking);

        // Se o pet não está onde queria estar
        if (keepSearching)
        {
            Debug.Log("Pet muda de scene e continua a busca..." + temp_name);
            // Inicia a animação de transição de scene
            StartCoroutine(gameObject.GetComponent<Invisible>().PetChangeLocation(temp_name));
            yield return new WaitForSeconds(0.5f);
            // Posiciona o pet na posição da porta por onde ele deve sair (mantendo as posições y e z atuais do pet)
            Vector3 petPosition = pet.gameObject.transform.position;
            pet.gameObject.transform.position = new Vector3(newStartPosition, petPosition.y, petPosition.z);
            
            // Chama novamente a função de movimento
            StartCoroutine(MoveToPosition(element, functionToCall));
        }
        else
        {
            Debug.Log("Pet chegou no local marcado");
            // Se ele está no local que queria, chama a função que foi passada como parâmetro
            StartCoroutine(functionToCall());
        }
    }
    
    public void SetHungryOnDelegateBool(bool value)
    {
        hungryOnDelegate = value;
    }

    public void SetThirstyOnDelegateBool(bool value)
    {
        thirstyOnDelegate = value;
    }
    
    public IEnumerator WaitForPetEndAction()
    {
        yield return new WaitUntil(() => !isPetDoingSomething);
    }

    private void IncreaseChanceCalculate()
    {
        double time = TimeManager.instance.GetTimeBetweenPeriods(TimeManager.instance.GetCurrentPeriod());
        time *= 0.8f;
        double maxActTime = time / pet.drinkTimes[TimeManager.instance.GetCurrentPeriod()];
        maxActTime /= 15f;
        increaseChanceToBeThirsty = (float) maxActTime;
        increaseChanceToBeThirsty *= Time.deltaTime;
    }

    public void RestartDrinkCount()
    {
        drinkCount = pet.drinkTimes[TimeManager.instance.GetCurrentPeriod()];
    }

    #endregion
}