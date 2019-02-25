using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextController", menuName = "My Assets/Text Controller")]
public class TextController : ScriptableObject
{
    /// <summary>
    /// Função que salva o nome digitado no SaveManager e chama a verificação de liberação do botão
    /// </summary>
    public void EndTextEdit()
    {
        SaveManager.player.nome = MenuManager.playerName.text;
        MenuManager.ReadyToGo();
    }
}
