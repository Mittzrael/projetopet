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
    protected int maxRandom = 1;
    protected int randomNumber;

    protected float randomActionCountdown = 0;
    public int maxIdleTime = 3;

    public float moveRangeMin = -20;
    public float moveRangeMax = 20;
    public float moveRangeMultiplier = 50;

    private DogMitza petAnimationScript;

    // Valores minimo e máximo para o tempo aleátorio de verificação das necessidades do pet
    private float actionMinRandom = 1;
    private float actionMaxRandom = 5;
    // Variável que armazena o valor aleátorio gerado
    private float actionRandom;

    private delegate void PetAction();
    private PetAction petActionList = null;
    private System.Delegate[] petDelegateList;
    public bool hungryOnDelegate = false;
    public bool thirstyOnDelegate = false;
    public bool sadOnDelegate = false;
    public bool peeOnDelegate = false;
    public bool poopOnDelegate = false;

    private delegate void FeedingAction(Food food);
    private delegate void IndependentAction();
    private FeedingAction lastFeedingAction = null;
    private FeedingAction testFeedingAction = null;
    private IndependentAction lastIndependentAction = null;
    private IndependentAction testIndependentAction = null;

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

        DontDestroyOnLoad(gameObject);

        player = SaveManager.instance.player; // Para testes

        petAnimationScript = gameObject.GetComponent<DogMitza>();
        petHealth = SaveManager.instance.player.pet.health;
        pet = SaveManager.instance.player.pet;
        StartCoroutine(PetActionVerifier());
    }

    private IEnumerator PetActionVerifier()
    {
        actionRandom = Random.Range(actionMinRandom, actionMaxRandom);
        
        //Debug.Log(actionRandom);
        /*float r = Random.Range(0f, 1f);
        Debug.Log("min: 0.1, max: 0.5, random: " + r + ", chance: " + ChanceToHappen(0.1f, 0.5f, r));
        
        testFeedingAction = pet.Eat;
        testFeedingAction += pet.Drink;

        Debug.Log(testFeedingAction.ToString());
        testFeedingAction(food);

        testFeedingAction -= pet.Eat;
        Debug.Log(testFeedingAction.ToString());

        testFeedingAction(food);

        testFeedingAction -= pet.Drink;
        if (testFeedingAction == null)
        {
            Debug.Log("NULL uhul");
        }
        else
        {
            Debug.Log(testFeedingAction.ToString());
            testFeedingAction(food);
        }*/

        if (petHealth.GetHungry() < healthLimit.GetHungry() && !hungryOnDelegate)
        {
            petActionList += PetHungry;
            hungryOnDelegate = true;
        }
        if (petHealth.GetThirst() < healthLimit.GetThirst() && !thirstyOnDelegate)
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
        if (petHealth.GetHapiness() < healthLimit.GetHapiness() && !sadOnDelegate)
        {
            petActionList += PetSad;
            sadOnDelegate = true;
        }

        if (petActionList != null)
        {
            Debug.Log("O que tinha...");
            petActionList();
            Debug.Log("O pet está....");
            petDelegateList = petActionList.GetInvocationList();
            petDelegateList[0].DynamicInvoke();
            Debug.Log("Removi...");
            petActionList -= petDelegateList[0] as PetAction;
            //System.Delegate.RemoveAll(petActionList, petDelegateList[0]);
            petDelegateList[0].DynamicInvoke();
            Debug.Log("Sobrou...");
            petActionList();
        }

        /*
        if (testFeedingAction != null)
        {
            System.Delegate[] lista = testFeedingAction.GetInvocationList();
            //lista[0].DynamicInvoke(food);

            Debug.Log("Inicio teste");
            Debug.Log("Primeiro teste");
            testFeedingAction(food);
            testFeedingAction -= lista[0] as FeedingAction;
            Debug.Log("Segundo teste");
            lastFeedingAction = testFeedingAction;
            testFeedingAction(food);
            Debug.Log("Terceiro teste");
            lista[0].DynamicInvoke(food);
            Debug.Log("Quarto teste");
            testFeedingAction += lista[0] as FeedingAction;
            testFeedingAction(food);
            Debug.Log("Fim teste");
            testFeedingAction = null;
        }
        if (testIndependentAction != null)
        {
            lastIndependentAction = testIndependentAction;
            testIndependentAction();
            testIndependentAction = null;
        }*/

        yield return new WaitForSeconds(actionRandom);
        StartCoroutine(PetActionVerifier());
    }

    private void PetHungry()
    {
        Debug.Log("Fome");
        //hungryOnDelegate = false;
    }

    private void PetThisty()
    {
        Debug.Log("Sede");
        //thirstyOnDelegate = false;
    }

    private void PetSad()
    {
        Debug.Log("Trste");
        //sadOnDelegate = false;
    }

    private void PetPee()
    {
        Debug.Log("Pee");
        //peeOnDelegate = false;
    }

    private void PetPoop()
    {
        Debug.Log("Poop");
       // poopOnDelegate = false;
    }

    //private void NotNullDelegate(Food food) { }
    //private void NotNullDelegate() { }

    /// <summary>
    /// Calcula a chance de algo acontecer, em porcentagem
    /// </summary>
    /// <param name="min">Limite inferior do atributo</param>
    /// <param name="max">Limite superior do atributo</param>
    /// <param name="value">Valor do atributo para verificar a chance</param>
    /// <returns>Retorna a chance em porcentagem, entre 0 e 1</returns>
    private float ChanceToHappen(float min, float max, float value)
    {
        return (max - value) / (max - min);
    }

    private IEnumerator PetActionVerifier_1 ()
    {
        int petNeed = VerifyPetNeed();

        switch (petNeed)
        {
            case -1:
                PetRandomMove();
                break;
            case 0:
                Debug.Log("Fome");
                break;
            case 1:
                Debug.Log("Sede");
                break;
            case 2:
                Debug.Log("Xixi errado");
                break;
            case 3:
                Debug.Log("Xixi certo");
                break;
            case 4:
                Debug.Log("Cocô errado");
                break;
            case 5:
                Debug.Log("Cocô certo");
                break;
            case 6:
                petAnimationScript.ToggleSad();
                Debug.Log("Brincar errado");
                break;
            case 7:
                petAnimationScript.ToggleSad();
                Debug.Log("Brincar certo");
                break;
        }

        

        yield return new WaitForSeconds(timeBetweenAction);
        if (petNeed != 6 || petNeed != 7)
        {
            yield return new WaitUntil (() => petAnimationScript.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("dog_mitza_cocando / idle"));
        }

        StartCoroutine(PetActionVerifier());
    }

    private int VerifyPetNeed()
    {
        if (petHealth.GetHungry() <= hungryWarning)
        {
            ThinkingBallon.CreateThinking(gameObject, "Ration");
            if (petHealth.GetHungry() <= healthLimit.GetHungry()) return 0;
        }
        if (petHealth.GetThirst() <= thirstyWarning)
        {
            ThinkingBallon.CreateThinking(gameObject, "Water");
            if (petHealth.GetThirst() <= healthLimit.GetThirst()) return 1;
        }
        if (petHealth.GetPee() >= healthLimit.GetPee())
        {
            if (petHealth.GetWhereToPP() < healthLimit.GetWhereToPP()) return 2;
            else return 3;
        }
        else if (petHealth.GetPoop() >= healthLimit.GetPoop())
        {
            if (petHealth.GetWhereToPP() < healthLimit.GetWhereToPP()) return 4;
            else return 5;
        }
        else if (petHealth.GetHapiness() <= healthLimit.GetHapiness())
        {
            if (petHealth.GetWhatToPlay() < healthLimit.GetWhatToPlay()) return 6;
            else return 7;
        }
        else return -1;
    }

    public void PetRandomMove()
    {
        if (randomActionCountdown >= maxIdleTime)
        {
            randomActionCountdown = 0;
            randomNumber = Random.Range(0, maxRandom + 1);
            switch (randomNumber)
            {
                case 0:
                    Debug.Log("Movimento Aleátorio - Coçando");
                    petAnimationScript.Cocando();
                    break;
                case 1:
                    float move = Random.Range(moveRangeMin, moveRangeMax) * moveRangeMultiplier;
                    Debug.Log("Movimento Aleátorio - Andando " + move);
                    petAnimationScript.MoveAnimalAndando(move);
                    break;
            }
        }
        else
        {
            if (petHealth.GetHapiness() < 0.5f)
            {
                randomActionCountdown += 0.5f;
            }
            else
            {
                randomActionCountdown++;
            }
        }
        Debug.Log(randomActionCountdown);
    }

    /// <summary>
    /// Para todas as corrotinas neste script
    /// </summary>
    public void StoptPetCoroutines()
    {
        StopAllCoroutines();
    }
}