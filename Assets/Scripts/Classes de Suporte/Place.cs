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
    public string name;
    /// <summary>
    /// Locais onde os itens estarão
    /// </summary>
    public List<Vector3> place = new List<Vector3>();
    /// <summary>
    /// Menores valores em que um objeto pode ser instanciado dentro da scene (limites de posição inferiores).
    /// </summary>
    public Vector3 minValor = new Vector3();
    /// <summary>
    /// Maiores valor em que um objeto pode ser instanciado dentro da scene (limites de posição superiores).
    /// </summary>
    public Vector3 maxValor = new Vector3();

    public Vector3 RandomPosition()
    {
        Vector3 position = new Vector3();
        position.x = UnityEngine.Random.Range(minValor.x, maxValor.x);
        position.y = UnityEngine.Random.Range(minValor.y, maxValor.y);
        position.z = UnityEngine.Random.Range(minValor.z, maxValor.z);
        return position;
    }
}
