using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script anexado ao pote de comida para ativar as interações e opções existentes ao clicar no mesmo
/// </summary>
public class FoodPotInteractions : MonoBehaviour
{
    private PetBasicAI petBasicAI;

    private void Awake()
    {
        petBasicAI = GameObject.FindGameObjectWithTag("PetFather").GetComponent<PetBasicAI>();
    }

    /// <summary>
    /// Função que realiza as ações quando o pote de comida é selecionado
    /// </summary>
    private void OnMouseDown()
    {
        if (SaveManager.instance.player.health.GetCleanFoodPot())
        {
            Debug.Log("Abriu menu de interação com o pote");
            Debug.Log("Foi para minigame");
            Debug.Log("Colocou comida");

            FoodPotCleaned();
        }
    }

    /// <summary>
    /// Função que avisa o pet que o pote de comida está limpo e com comida, permitindo que ele coma
    /// </summary>
    public void FoodPotCleaned()
    {
        StartCoroutine(petBasicAI.PetGoEat());
    }
}
