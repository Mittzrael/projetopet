using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe filha de MenuManager própria para exibição das residencias
/// </summary>
public class HomeMenuManager : MenuManager
{
    public ResidenciaList residenciaList;

    private void Awake()
    {
        // Informa o tamanho da lista de residencias ao MenuManager
        listSize = residenciaList.residencia.Length;
    }

    /// <summary>
    /// Mostra as informações das residencias
    /// </summary>
    /// <param name="i"></param>
    public override void DisplayItemInfo(int i)
    {
        itemInstance.GetComponentInChildren<RawImage>().texture = residenciaList.residencia[i].itemImage;
        itemInstance.GetComponentInChildren<Text>().text = residenciaList.residencia[i].residenciaInfo;
    }
}
