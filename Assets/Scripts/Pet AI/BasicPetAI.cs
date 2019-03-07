using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPetAI : MonoBehaviour
{
    [Tooltip("Tempo entre as verificações de ações do Pet (em segundos)")]
    public float timeBetweenAction = 1;
    [Tooltip("Limite para a fome do Pet (entre 0 e 1)")]
    public float hungry = 0.2f;
    [Tooltip("Limite para a sede do Pet (entre 0 e 1)")]
    public float thristy = 0.2f;
    [Tooltip("Limite para xixi do Pet (entre 0 e 1)")]
    public float pee = 0.8f;
    [Tooltip("Limite para coco do Pet (entre 0 e 1)")]
    public float poop = 0.8f;
    [Tooltip("Limite para a educação do Pet (necessidades) (entre 0 e 1)")]
    public float hygieneTrain = 0.9f;
    [Tooltip("Limite para a felicidade do Pet (entre 0 e 1)")]
    public float hapiness = 0.2f;
    [Tooltip("Limite para a educação do Pet (brinquedos) (entre 0 e 1)")]
    public float careTrain = 0.9f;

    public static BasicPetAI instance;
    public Player player;

    protected Health petHealth;
    float i = 0;

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

        player = SaveManager.instance.player;
        StartCoroutine(PetActionVerifier());
    }

    private IEnumerator PetActionVerifier ()
    {
        petHealth = SaveManager.instance.player.health;

        i += timeBetweenAction;
        //Debug.Log("esperando " + i);

        switch (VerifyPetNeed())
        {
            case -1:
                Debug.Log("Movimento Aleátorio");
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
                Debug.Log("Brincar errado");
                break;
            case 7:
                Debug.Log("Brincar certo");
                break;
        }

        yield return new WaitForSeconds(timeBetweenAction);
        StartCoroutine(PetActionVerifier());
    }

    private int VerifyPetNeed()
    {
        //int petNeedIndex = (int) Random.Range(-1, 7);

        if (petHealth.GetHungry() <= hungry) return 0;
        else if (petHealth.GetThirst() <= thristy) return 1;
        else if (petHealth.GetPee() >= pee)
        {
            if (petHealth.GetHygieneTrain() < hygieneTrain) return 2;
            else return 3;
        }
        else if (petHealth.GetPoop() >= poop)
        {
            if (petHealth.GetHygieneTrain() < hygieneTrain) return 4;
            else return 5;
        }
        else if (petHealth.GetCare() <= hapiness)
        {
            if (petHealth.GetCareTrain() < careTrain) return 6;
            else return 7;
        }
        else return -1;

        //return petNeedIndex;
    }

    public void StoptPetCoroutines()
    {
        StopAllCoroutines();
    }
}