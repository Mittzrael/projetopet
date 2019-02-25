using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe filha de ItemList para a lista de avatares (ScriptableObjects)
/// </summary>
[CreateAssetMenu(fileName = "ItemList", menuName = "My Assets/Lista de Avatares")]
public class AvatarList : ItemList
{
    public Avatar[] avatar;
}
