using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenuManager : MenuManager
{
    public ResidenciaList residenciaList;

    private void Awake()
    {
        listSize = residenciaList.residencia.Length;
    }

    public override void DisplayItemInfo(int i)
    {
        itemInstance.GetComponentInChildren<RawImage>().texture = residenciaList.residencia[i].itemImage;
        itemInstance.GetComponentInChildren<Text>().text = residenciaList.residencia[i].residenciaInfo;
    }
}
