using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe filha de ItemList para a lista de residencias (ScriptableObjects)
/// </summary>
[CreateAssetMenu(fileName = "ItemList", menuName = "My Assets/Lista de Residencias")]
public class ResidenciaList : ItemList
{
    public Residencia[] residencia;
}
