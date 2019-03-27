using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Flag
{
    [Tooltip("Descrição da flag")]
    /// <summary>
    /// Descrição da flag.
    /// </summary>
    public string name;
    [Tooltip("Estado atual da flag")]
    /// <summary>
    /// Estado atual da flag.
    /// </summary>
    public bool state;
}
