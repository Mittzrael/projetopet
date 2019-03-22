using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Classe filha de MenuManager própria para exibição dos avatares
/// </summary>
public class AvatarMenuManager : MenuManager
{
    public AvatarList avatarList;

    private void Awake()
    {
        // Informa o tamanho da lista de avatares ao MenuManager
        SaveManager.instance.CreateNewPlayer();
        listSize = avatarList.avatar.Length;
        playerName = GameObject.FindGameObjectWithTag("InputBox").GetComponent<InputField>();
    }

    /// <summary>
    /// Mostra as informações dos avatares
    /// </summary>
    /// <param name="i"></param>
    public override void DisplayItemInfo(int i)
    {
        itemInstance.GetComponent<RawImage>().texture = avatarList.avatar[i].itemImage;
    }
}
