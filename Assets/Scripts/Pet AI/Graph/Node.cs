// Retirado de https://docs.microsoft.com/en-us/previous-versions/ms379572%28v%3dvs.80%29#understanding-binary-trees
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe que cria um nó genérico para uso em árvores e grafos
public class Node<T>
{
    // Variável armazenada no nó
    private T data;
    // Vizinhos do nó (quem ele tem acesso)
    private NodeList<T> neighbors = null;

    // Construtores do nó
    public Node() { }
    public Node(T data) : this(data, null) { }
    public Node(T data, NodeList<T> neighbors)
    {
        this.data = data;
        this.neighbors = neighbors;
    }

    // Get/Set do valor do nó
    public T Value
    {
        get
        {
            return data;
        }
        set
        {
            data = value;
        }
    }

    // Get/Set dos vizinhos do nó
    public NodeList<T> Neighbors
    {
        get
        {
            return neighbors;
        }
        set
        {
            neighbors = value;
        }
    }
}
