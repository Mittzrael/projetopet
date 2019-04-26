using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script anexado ao pote de água para ativar as interações e opções existentes ao clicar no mesmo
/// </summary>
public class WaterPotInteractions : MonoBehaviour
{
    /// <summary>
    /// Função que realiza as ações quando o pote de água é selecionado
    /// </summary>
    private void OnMouseDown()
    {
        if (SaveManager.instance.player.health.GetCleanWaterPot())
        {
            Debug.Log("Abriu menu de interação com o pote");
            Debug.Log("Foi para minigame");
            Debug.Log("Colocou água");

            FoodPotCleaned();
        }
    }

    /// <summary>
    /// Função que remove o aviso de necessidade de limpeza do pote de água
    /// </summary>
    public void FoodPotCleaned()
    {
        SaveManager.instance.player.health.PutInCleanWaterPot(false);
        PopUpWarning.instance.SolveWarning("CleanWaterPot");
    }
}
