using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe filha de ItemList para a lista de pets (ScriptableObjects)
/// </summary>
[CreateAssetMenu(fileName = "ItemList", menuName = "My Assets/Lista de Pets")]
public class PetList : ItemList
{
    public Pet[] pet;
}
