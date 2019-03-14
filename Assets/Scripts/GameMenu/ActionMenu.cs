using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script para o menu de ações 
/// Centraliza as ações de interação com o pet
/// </summary>

public class ActionMenu : MonoBehaviour
{
    public void OnBtnChamarPetClick()
    {
        Debug.Log("Chamar pet");
    }
    public void OnBtnAcariciarPetClick()
    {
        Debug.Log("Acariciar pet");
    }
    public void OnBtnEnsinarPetClick()
    {
        Debug.Log("Ensinar pet");
    }
}
