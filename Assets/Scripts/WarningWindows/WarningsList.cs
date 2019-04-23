using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject que é utilizado para armazenar os avisos que serão mostrados
/// </summary>
[CreateAssetMenu(fileName = "WarningsList", menuName = "My Assets/Lista de Avisos PopUp")]
public class WarningsList : ItemList
{
    public Warnings[] warnings;
}
