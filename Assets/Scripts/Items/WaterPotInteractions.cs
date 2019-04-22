using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPotInteractions : MonoBehaviour
{
    //private PetMovement petMovement;

    //private void Awake()
    //{
    //    petMovement = GameObject.Find("PetFather").GetComponent<PetMovement>();
    //}

    private void OnMouseDown()
    {
        if (SaveManager.instance.player.health.GetThirsty())
        {
            Debug.Log("Abriu menu de interação com o pote");
            Debug.Log("Foi para minigame");
            Debug.Log("Colocou água");

            FoodPotCleaned();
        }
    }

    public void FoodPotCleaned()
    {
        PopUpWarning.instance.SolveWarning("Thirsty");
    }
}
