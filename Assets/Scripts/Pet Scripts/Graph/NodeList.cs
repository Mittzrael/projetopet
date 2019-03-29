// Retirado de https://docs.microsoft.com/en-us/previous-versions/ms379572%28v%3dvs.80%29#understanding-binary-trees
// Adaptado por Rafael
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

// Classe que gera uma lista de nós genéricos
public class NodeList<T> : Collection<Node<T>>
{
    /// <summary>
    ///  Costrutor (base chama o construtor de Collection<> - pai de NodeList<>)
    /// </summary>
    public NodeList() : base() { }

    /// <summary>
    /// Construtor que gera uma lista contendo uma quantidade inicial de elementos do tipo Node<typeparamref name="T"/>
    /// </summary>
    /// <param name="initialSize"></param>
    public NodeList(int initialSize)
    {
        for (int i = 0; i < initialSize; i++)
            base.Items.Add(default(Node<T>)); // Adiciona um Node<> a lista (base.Items.Add - função herdada do pai Collection)
    }

    /// <summary>
    /// Função que encontra um nó do tipo Node<> que contenha o valor value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public Node<T> FindByValue_Base(T value)
    {
        foreach (Node<T> node in Items)
            if (node.Value.Equals(value))
                return node;

        return null;
    }

    /// <summary>
    /// Função que encontra um nó do tipo GraphNode<> que contenha o valor value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public GraphNode<T> FindByValue(T value)
    {
        // Para cada nó do tipo GraphNode em Items (lista de nós) verifica se o valor armazenado é igual ao procurado
        // Se for, retorna o nó
        foreach (GraphNode<T> node in Items)
            if (node.Value.Equals(value))
                return node;

        // Caso não encontre, retorna null
        return null;
    }
}
