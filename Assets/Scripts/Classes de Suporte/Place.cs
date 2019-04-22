using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Place
{
    /// <summary>
    /// Nome do cômodo (scene)
    /// </summary>
    [Tooltip("Nome do cômodo (scene)")]
    public string name;
    /// <summary>
    /// Locais onde os itens estarão
    /// </summary>
    [Tooltip("Locais onde outros itens estarão")]
    public List<Vector3> place = new List<Vector3>();
}
