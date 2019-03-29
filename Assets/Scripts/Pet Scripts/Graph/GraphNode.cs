// Retirado de https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)#datastructures20_5_topic3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe que cria um nó genérico para Grafos
public class GraphNode<T> : Node<T>
{
    // Lista de custo/peso das arestas do nó
    private List<int> costs;
    // Nó antecessor a este (utilizado para traçar caminhos/rotas)
    public GraphNode<T> prevNode = null;

    // Construtores
    public GraphNode() : base() { }
    public GraphNode(T value) : base(value) { }
    public GraphNode(T value, NodeList<T> neighbors) : base(value, neighbors) { }

    // Get/Set dos vizinhos do nó
    new public NodeList<T> Neighbors
    {
        get
        {
            // Caso não tenha vizinhos, gera uma lista vazia de nós (NodeList)
            if (base.Neighbors == null)
                base.Neighbors = new NodeList<T>();

            return base.Neighbors;
        }
    }

    // Get/Set dos custos/peso das arestas do nó
    public List<int> Costs
    {
        get
        {
            // Caso não tenha custos/pesos, gera uma lista vazia
            if (costs == null)
                costs = new List<int>();

            return costs;
        }
    }
}
