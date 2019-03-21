using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPetAI : MonoBehaviour
{
    [Tooltip("Tempo entre as verificações de ações do Pet (em segundos)")]
    public float timeBetweenAction = 1;

    [Tooltip("Valores limitantes para os atributos do pet")]
    public Health healthLimit;
    [Tooltip("Valor para aviso de fome")]
    public float hungryWarning;
    [Tooltip("Valor para aviso de sede")]
    public float thirstyWarning;

    public static BasicPetAI instance;
    public Player player; // Para testes

    protected Health petHealth;
    protected int randomNumber;

    protected float randomActionCountdown = 0;
    [Tooltip("Quantidade de vezes que o pet verifica que não há nada mais importante para fazer antes de fazer algum movimento aleatório")]
    public int maxIdleTime = 5;

    [Tooltip("Range de liberdade de movimento do pet (min = esquerda)")]
    public float moveRangeMin = -20;
    [Tooltip("Range de liberdade de movimento do pet (max = direita)")]
    public float moveRangeMax = 20;
    [Tooltip("Multiplicador para o range do movimento")]
    public float moveRangeMultiplier = 50;

    // Nome do script com as animações do pet - para poder acessar as animações
    private DogMitza petAnimationScript;

    // Valores minimo e máximo para o tempo aleatório de verificação das necessidades do pet
    private float actionMinRandom = 1;
    private float actionMaxRandom = 5;
    // Variável que armazena o valor aleatório gerado
    private float actionRandom;

    // DELEGATE: variável que pode armazenar funções
    // Utilizada para armazenar as funções correspondentes as ações que o pet quer realizar
    // As funções serão chamadas através do inicializador da DELEGATE, de modo genérico
    private delegate void PetAction();
    private PetAction petActionList = null;
    // Vetor de delegates (utilizado para chamar uma delegate de cada vez no pacote de delegates)
    private System.Delegate[] petDelegateList;

    // Variáveis utilizadas para bloquear uma ação que já está armazenada na delegate e ainda não foi chamada
    private bool hungryOnDelegate = false;
    private bool hungryWarningOnDelegate = false;
    private bool thirstyOnDelegate = false;
    private bool thirstyWarningOnDelegate = false;
    private bool sadOnDelegate = false;
    private bool peeOnDelegate = false;
    private bool poopOnDelegate = false;

    private Pet pet;
    private Food food;

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
        //pet = SaveManager.instance.player.pet;
        petHealth = SaveManager.instance.player.health;
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

        // VERIFICAÇÃO DO ESTADO DO PET
        // Verifica se um status específico está fora do limite aceitável para o pet e se a ação já está na lista
        // Se estiver fora do limite e não estiver na lista de ações, adiciona na lista (delegate) e sinaliza como adicionado
        if (petHealth.GetHungry() < healthLimit.GetHungry() && !hungryOnDelegate)
        {
            petActionList += PetHungry;
            hungryOnDelegate = true;
        }
        if (petHealth.GetHungry() < hungryWarning && !hungryWarningOnDelegate)
        {
            petActionList += PetHungryWarnig;
            hungryWarningOnDelegate = true;
        }
        if (petHealth.GetThirsty() < healthLimit.GetThirsty() && !thirstyOnDelegate)
        {
            petActionList += PetThisty;
            thirstyOnDelegate = true;
        }
        if (petHealth.GetThirsty() < thirstyWarning && !thirstyWarningOnDelegate)
        {
            petActionList += PetThirstyWarning;
            thirstyWarningOnDelegate = true;
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
            petDelegateList[0].DynamicInvoke();
            // Remove esta função da lista de funções da delegate
            petActionList -= petDelegateList[0] as PetAction;
        }
        // Caso contrário, chama a função que realiza movimentos aleatórios
        else
        {
            PetRandomMove();
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
    private void PetHungry()
    {
        Debug.Log("Fome");
        hungryOnDelegate = false;
    }

    /// <summary>
    /// Função que gera um aviso visual de fome quando entra na zona de aviso de fome
    /// </summary>
    private void PetHungryWarnig()
    {
        Debug.Log("Msg fome");
        hungryWarningOnDelegate = false;
    }

    /// <summary>
    /// Função que comanda o pet quando ele está com sede
    /// </summary>
    private void PetThisty()
    {
        Debug.Log("Sede");
        thirstyOnDelegate = false;
    }

    /// <summary>
    /// Função que gera um aviso visual de sede quando entra na zona de aviso de sede
    /// </summary>
    private void PetThirstyWarning()
    {
        Debug.Log("Msg sede");
        thirstyWarningOnDelegate = false;
    }

    /// <summary>
    /// Função que comanda o pet quando ele está triste/carente
    /// </summary>
    private void PetSad()
    {
        Debug.Log("Trste");
        sadOnDelegate = false;
    }

    /// <summary>
    /// Função que controla o pet quando ele está com vontade de fazer xixi
    /// </summary>
    private void PetPee()
    {
        Debug.Log("Pee");
        peeOnDelegate = false;
    }

    /// <summary>
    /// Função que controla o pet quando ele está com vontade de fazer coco
    /// </summary>
    private void PetPoop()
    {
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
    
    public void PetRandomMove()
    {
        if (randomActionCountdown >= maxIdleTime)
        {
            randomActionCountdown = 0;
            randomNumber = Random.Range(0, 2);
            switch (randomNumber)
            {
                case 0:
                    Debug.Log("Movimento aleatório - Coçando");
                    petAnimationScript.Cocando();
                    break;
                case 1:
                    float move = Random.Range(moveRangeMin, moveRangeMax) * moveRangeMultiplier;
                    Debug.Log("Movimento aleatório - Andando " + move);
                    petAnimationScript.MoveAnimalAndando(move);
                    break;
            }
        }
        else
        {
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