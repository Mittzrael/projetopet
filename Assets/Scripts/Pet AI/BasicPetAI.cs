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
    public Player player;

    protected Health petHealth;
    protected int maxRandom = 1;
    protected int randomNumber;

    protected float randomActionCountdown = 0;
    public int maxIdleTime = 3;

    public float moveRangeMin = -20;
    public float moveRangeMax = 20;
    public float moveRangeMultiplier = 50;

    private DogMitza petAnimationScript;
    
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

        player = SaveManager.instance.player;

        petAnimationScript = gameObject.GetComponent<DogMitza>();
        petHealth = SaveManager.instance.player.pet.health;
        StartCoroutine(PetActionVerifier());
    }

    private IEnumerator PetActionVerifier ()
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
            //ThinkingBallon.CreateThinking(gameObject, "Water");
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
        //Debug.Log(randomActionCountdown);
    }

    /// <summary>
    /// Para todas as corrotinas neste script
    /// </summary>
    public void StoptPetCoroutines()
    {
        StopAllCoroutines();
    }
}