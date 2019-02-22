using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetMenuManager : MenuManager
{
    public PetList petList;

    private void Awake()
    {
        listSize = petList.pet.Length;
    }

    public override void DisplayItemInfo(int i)
    {
        itemInstance.GetComponentInChildren<RawImage>().texture = petList.pet[i].itemImage;
        itemInstance.GetComponentInChildren<Text>().text = petList.pet[i].petInfo;
    }
}
