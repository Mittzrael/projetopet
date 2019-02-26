using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextController", menuName = "My Assets/Text Controller")]
public class TextController : ScriptableObject
{
    public void EndTextEdit()
    {
        SaveManager.instance.player.nome = MenuManager.playerName.text;
        MenuManager.ReadyToGo();
    }
}
