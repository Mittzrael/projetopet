using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPotInteractions : MonoBehaviour
{
    private PetMovement petMovement;

    private void Awake()
    {
        petMovement = GameObject.Find("PetFather").GetComponent<PetMovement>();
    }

    private void OnMouseDown()
    {
        if (SaveManager.instance.player.health.GetHungry())
        {
            Debug.Log("Abriu menu de interação com o pote");
            Debug.Log("Foi para minigame");
            Debug.Log("Colocou comida");

            FoodPotCleaned();
        }
    }

    public void FoodPotCleaned()
    {
        StartCoroutine(petMovement.PetGoEat());
    }
}
