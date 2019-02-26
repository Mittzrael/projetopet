using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe filha de MenuManager própria para exibição dos pets
/// </summary>
public class PetMenuManager : MenuManager
{
    public PetList petList;

    private void Awake()
    {
        // Informa o tamanho da lista de pets ao MenuManager
        listSize = petList.pet.Length;
    }

    /// <summary>
    /// Mostra as informações dos pets
    /// </summary>
    /// <param name="i"></param>
    public override void DisplayItemInfo(int i)
    {
        itemInstance.GetComponentInChildren<RawImage>().texture = petList.pet[i].itemImage;
        itemInstance.GetComponentInChildren<Text>().text = petList.pet[i].petInfo;
    }
}
