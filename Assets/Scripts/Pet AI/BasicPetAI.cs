using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicPetAI : MonoBehaviour
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

    public static BasicPetAI instance; // Garantir a unicidade

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
    private DogMitza petAnimationScript;

    // Valores minimo e máximo para o tempo aleatório de verificação das necessidades do pet
    readonly private float actionMinRandom = 1;
    readonly private float actionMaxRandom = 5;
    // Variável que armazena o valor aleatório gerado
    private float actionRandom;

    // DELEGATE: variável que pode armazenar funções
    // Utilizada para armazenar as funções correspondentes as ações que o pet quer realizar
    // As funções serão chamadas através do inicializador da DELEGATE, de modo genérico
    private delegate IEnumerator PetAction();
    private PetAction petActionList = null;
    // Vetor de delegates (utilizado para chamar uma delegate de cada vez no pacote de delegates)
    private System.Delegate[] petDelegateList;

    #region Variáveis utilizadas para bloquear uma ação que já está armazenada na delegate e ainda não foi chamada
    private bool hungryOnDelegate = false;
    private bool hungryWarningOnDelegate = false;
    private bool thirstyOnDelegate = false;
    private bool thirstyWarningOnDelegate = false;
    private bool sadOnDelegate = false;
    private bool peeOnDelegate = false;
    private bool poopOnDelegate = false;
    #endregion

    #region Variáveis para o grafo de locais de acesso do pet
    [SerializeField]
    public GraphCreator[] petAccessInfo;
    private int petAccessInfoIndex;
    [SerializeField]
    private Graph<string> petAccessGraph;
    #endregion

    // Variável que indica se o pet já está realizando alguma ação (não permite que várias ações sejam execurtadas em paralelo)
    private bool isPetDoingSometring = false;

    #region Para testes
    private Pet pet;
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

        player = SaveManager.instance.player; // Para testes

        petAnimationScript = gameObject.GetComponent<DogMitza>();
        petHealth = SaveManager.instance.player.health;

        petAccessInfoIndex = PetAccessListSelection();
        petAccessGraph = petAccessInfo[petAccessInfoIndex].CreateGraph();

        StartCoroutine(PetActionVerifier());
    }
    
    public int PetAccessListSelection()
    {
        return 0;
    }

    /// <summary>
    /// Função que verifica que ação o pet irá realizar baseado no estado atual do pet e no que ele já realizou anteriormente
    /// </summary>
    /// <returns></returns>
    private IEnumerator PetActionVerifier()
    {
        // Número aleatório gerado para definir quando ocorrerá a próxima verificação
        actionRandom = Random.Range(actionMinRandom, actionMaxRandom);

        if (!isPetDoingSometring)
        {
            // VERIFICAÇÃO DO ESTADO DO PET
            // Verifica se um status específico está fora do limite aceitável para o pet e se a ação já está na lista
            // Se estiver fora do limite e não estiver na lista de ações, adiciona na lista (delegate) e sinaliza como adicionado
            if (petHealth.GetHungry() < healthLimit.GetHungry() && !hungryOnDelegate)
            {
                petActionList += PetHungry;
                hungryOnDelegate = true;
            }
            if (petHealth.GetThirsty() < healthLimit.GetThirsty() && !thirstyOnDelegate)
            {
                petActionList += PetThisty;
                thirstyOnDelegate = true;
            }
            if (petHealth.GetPoop() > healthLimit.GetPoop() && !poopOnDelegate)
            {
                petActionList += PetPoop;
                poopOnDelegate = true;
            }
            if (petHealth.GetPee() > healthLimit.GetPee() && !peeOnDelegate)
            {
                petActionList += PetPee;
                peeOnDelegate = true;
            }
            if (petHealth.GetHappiness() < healthLimit.GetHappiness() && !sadOnDelegate)
            {
                petActionList += PetSad;
                sadOnDelegate = true;
            }

            // Se a lista de ações do pet não estiver vazia, realiza a primeira ação da lista e a remove da lista
            if (petActionList != null)
            {
                // Discretiza a lista de ações da delegate em um vetor de delegates
                petDelegateList = petActionList.GetInvocationList();
                // Chama a função que está armazenada na primeira posição do vetor de delegates
                //petDelegateList[0].DynamicInvoke();
                // Chama a função que está armazenada na primeira posição do vetor de delegates como uma coroutine
                StartCoroutine(petDelegateList[0].Method.Name);
                // Remove esta função da lista de funções da delegate
                petActionList -= petDelegateList[0] as PetAction;
            }
            // Caso contrário, chama a função que realiza movimentos aleatórios
            else
            {
                PetRandomMove();
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
    private IEnumerator PetHungry()
    {
        Debug.Log("Fome");

        string currentScene = SceneManager.GetActiveScene().name;
        float movePosition;

        if (currentScene == player.foodLocationSceneName)
        {
            movePosition = player.foodLocationPosition.x;
        }
        else
        {
            Debug.Log(player.foodLocationSceneName);
            var path = petAccessGraph.BFS(currentScene, player.foodLocationSceneName);
            string[] name = HasHSetToString(path).Split(',');
            for (int i = name.Length - 1; i > 0; i--)
            {
                Debug.Log(name[i] + " -> " + petAccessInfo[petAccessInfoIndex].petAccessGraph.teste(name[i], name[i - 1]));
            }

            movePosition = petAccessInfo[petAccessInfoIndex].petAccessGraph.teste(name[name.Length - 1], name[name.Length - 2]);
        }

        petAnimationScript.MoveAnimalAndando(movePosition);

        yield return new WaitUntil(() => !petAnimationScript.isWalking);

        if (name.Length >= 2)
        {
            StartCoroutine(PetHungry());
            Debug.Log("Continua a busca");
        }
        else
        {
            //player.health.PutInHungry(1);
            Debug.Log("Comi");
            hungryOnDelegate = false;
        }
    }

    public string HasHSetToString(HashSet<string> hashSet)
    {
        return string.Join(",", hashSet);
    }

    public void PrintPath(string[] path)
    {
        for (int i = path.Length - 1; i >= 0; i--)
        {
            Debug.Log(path[i]);
        }
    }

    /// <summary>
    /// Função que gera um aviso visual de fome quando entra na zona de aviso de fome
    /// </summary>
    private IEnumerator PetHungryWarnig()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Msg fome");
        hungryWarningOnDelegate = false;
    }

    /// <summary>
    /// Função que comanda o pet quando ele está com sede
    /// </summary>
    private IEnumerator PetThisty()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Sede");
        thirstyOnDelegate = false;
    }

    /// <summary>
    /// Função que gera um aviso visual de sede quando entra na zona de aviso de sede
    /// </summary>
    private IEnumerator PetThirstyWarning()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Msg sede");
        thirstyWarningOnDelegate = false;
    }

    /// <summary>
    /// Função que comanda o pet quando ele está triste/carente
    /// </summary>
    private IEnumerator PetSad()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Trste");
        sadOnDelegate = false;
    }

    /// <summary>
    /// Função que controla o pet quando ele está com vontade de fazer xixi
    /// </summary>
    private IEnumerator PetPee()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Pee");
        peeOnDelegate = false;
    }

    /// <summary>
    /// Função que controla o pet quando ele está com vontade de fazer coco
    /// </summary>
    private IEnumerator PetPoop()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Poop");
        poopOnDelegate = false;
    }

    #endregion

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
    /// Função que controla os movimentos aleatórios do pet
    /// Movimentos que ocorrem quando o pet não possui nenhuma outra necessidade
    /// </summary>
    public void PetRandomMove()
    {
        // Verifica se o limite de vezes que a função deve ser chamada foi atingido
        if (randomActionCountdown >= maxIdleTime)
        {
            randomActionCountdown = 0;
            // Escolhe um valor aleatório para selecionar qual ação o pet irá realizar
            randomNumber = Random.Range(0, 2);
            switch (randomNumber)
            {
                case 0: // Pet se coça
                    Debug.Log("Movimento aleatório - Coçando");
                    petAnimationScript.Cocando();
                    break;
                case 1: // Pet se move até um ponto aleatório na scene
                    float move = Random.Range(moveRangeMin, moveRangeMax) * moveRangeMultiplier;
                    Debug.Log("Movimento aleatório - Andando " + move);
                    petAnimationScript.MoveAnimalAndando(move);
                    break;
            }
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

    /// <summary>
    /// Para todas as corrotinas neste script
    /// </summary>
    public void StoptPetCoroutines()
    {
        StopAllCoroutines();
    }
}