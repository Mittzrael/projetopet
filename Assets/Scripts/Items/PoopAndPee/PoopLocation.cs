using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Place
{
    public string name;
    public List<Vector3> place = new List<Vector3>();
}

[Serializable]
public class PoopLocation
{
    [SerializeField]
    private List<Place> location = new List<Place>();
    [SerializeField]
    private int poopCount; //Quantidade de poops no game.

    /// <summary>
    /// Retorna a quantidade de poop que estão em jogo, contando todas as scenes.
    /// </summary>
    /// <returns></returns>
    public int QuantityOfPees()
    {
        return poopCount;
    }

    /// <summary>
    /// Adiciona a posição a lista de vetores
    /// </summary>
    /// <param name="local">nome da scene</param>
    /// <param name="position">posição de onde o item está</param>
    public void Add(string local, Vector3 position)
    {
        int index = IndexPlace(local, location);

        if (index != -1)
        {
            location[index].place.Add(position);
        }
        else
        {
            Place vetor = new Place();
            vetor.name = local;
            vetor.place.Add(position);
            location.Add(vetor);
            poopCount++;
        }
    }

    /// <summary>
    /// Verifica se existe um lugar com o nome dado na lista dada e retorna a posição deste lugar
    /// </summary>
    /// <param name="local">string com o nome da scene</param>
    /// <param name="listaDeListas">lista onde isso será procurado</param>
    /// <returns></returns>
    private int IndexPlace(string local, List<Place> listaDeListas)
    {
        foreach (Place lista in listaDeListas)
        {
            if (lista.name.Equals(local))
            {
                return listaDeListas.IndexOf(lista);
            }
        }
        return -1;
    }

    /// <summary>
    /// Remove o item da lista do ambiente
    /// </summary>
    /// <param name="local">Nome da scene</param>
    /// <param name="position">Posição que o item possui</param>
    public void Remove(string local, Vector3 position)
    {
        int index = IndexPlace(local, location);

        if (index != -1)
        {
            location[index].place.Remove(position);
            poopCount--;
        }
    }

    /// <summary>
    /// Retorna a lista completa de itens do ambiente
    /// </summary>
    /// <param name="local">nome da scene</param>
    /// <returns></returns>
    public List<Vector3> View(string local)
    {
        int index = IndexPlace(local, location);
        if (index != -1)
        {
            return location[index].place;
        }
        return new List<Vector3>();
    }
}
