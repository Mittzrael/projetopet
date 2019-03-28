// Retirado de https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)#datastructures20_5_topic3
// Adaptado por Rafael
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe que gera um grafo genérico (pode ser direcionado ou não)
public class Graph<T> : IEnumerable<T>
{
    // Lista de nós do grafo
    private NodeList<T> nodeSet;

    // Construtor que cria um grafo vazio
    public Graph() : this(null) { }
    // Construtos que cria um grafo a partir de um conjunto de nós
    public Graph(NodeList<T> nodeSet)
    {
        if (nodeSet == null)
            this.nodeSet = new NodeList<T>();
        else
            this.nodeSet = nodeSet;
    }

    /// <summary>
    /// Adiciona um novo nó no grafo
    /// </summary>
    /// <param name="node"></param>
    public void AddNode(GraphNode<T> node)
    {
        nodeSet.Add(node);
    }

    /// <summary>
    /// Adiciona um novo nó no grafo
    /// </summary>
    /// <param name="value"></param>
    public void AddNode(T value)
    {
        nodeSet.Add(new GraphNode<T>(value));
    }

    /// <summary>
    /// Adiciona uma aresta direcionada no grafo
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="cost"></param>
    public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
    {
        from.Neighbors.Add(to);
        from.Costs.Add(cost);
    }

    /// <summary>
    /// Adiciona uma aresta direcionada no grafo sem custo
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    public void AddDirectedEdge(T from, T to, int d)
    {
        AddDirectedEdge(nodeSet.FindByValue(from), nodeSet.FindByValue(to), d);
    }

    /// <summary>
    /// Adiciona uma arestão não-direcionada no grafo
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="cost"></param>
    public void AddUndirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
    {
        from.Neighbors.Add(to);
        from.Costs.Add(cost);

        to.Neighbors.Add(from);
        to.Costs.Add(cost);
    }

    /// <summary>
    /// Verifica se o grafo contém um nó com o valor desejado
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool Contains(T value)
    {
        return nodeSet.FindByValue(value) != null;
    }

    /// <summary>
    /// Remove um nó e todas as suas arestas
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool Remove(T value)
    {
        // Encontra o nó que deve ser removido
        GraphNode<T> nodeToRemove = (GraphNode<T>)nodeSet.FindByValue(value);
        // Se ele não existir, retorna false
        if (nodeToRemove == null)
            return false;

        // Remove o nó do grafo
        nodeSet.Remove(nodeToRemove);

        // Para cada nó do grafo
        foreach (GraphNode<T> gnode in nodeSet)
        {
            int index = gnode.Neighbors.IndexOf(nodeToRemove);
            // Se o nó estiver na lista de vizinhos de algum nó do grafo, remove ele da lista de vizinho e o custo/peso da aresta
            if (index != -1)
            {
                gnode.Neighbors.RemoveAt(index);
                gnode.Costs.RemoveAt(index);
            }
        }

        return true;
    }

    // Get/Set da lista de nós do grafo
    public NodeList<T> Nodes
    {
        get { return nodeSet; }
    }

    // Get/Set do total de nós do grafo
    public int Count
    {
        get { return nodeSet.Count; }
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    // Retirado de: https://www.koderdojo.com/blog/breadth-first-search-and-shortest-path-in-csharp-and-net-core
    // Adaptado por Rafael
    /// <summary>
    /// Função que encontra o menor caminho entre <typeparamref name="start"/> e <typeparamref name="searchFor"/>
    /// </summary>
    /// <param name="start">Nó inicial da busca</param>
    /// <param name="searchFor">Nó a ser procurado</param>
    /// <returns></returns>
    public HashSet<T> BFS(T start, T searchFor)
    {
        // Conjunto de valores já visitados pelo algoritmo (construtor cria um conjunto vazio)
        var visited = new HashSet<T>();
        
        // Se o nó que se busca não está no grafo, retorna o conjunto vazio
        if (nodeSet.FindByValue(start) == null)
        {
            return visited;
        }

        // Conjunto de valores que representam o menor caminho entre start e searchFor
        var path = new HashSet<T>();
        // Fila dos nós a serem verificados
        var queue = new Queue<T>();
        queue.Enqueue(start);

        // Enquanto existir algum nó na fila, continua a busca
        while (queue.Count > 0)
        {
            // Pega o primeiro nó da fila
            var vertex = queue.Dequeue();
            // Se ele já existir no conjunto dos visitados, passa para o próximo nó
            if (visited.Contains(vertex))
                continue;

            // Se não estiver no conjunto, adiciona
            visited.Add(vertex);
            // Para cada vizinho do nó vertex, verifica se o vizinho já está no conjunto dos visitados
            foreach (var neighbor in nodeSet.FindByValue(vertex).Neighbors)
            {
                if (!visited.Contains(neighbor.Value))
                {
                    // Se não estiver, coloca ele na lista
                    queue.Enqueue(neighbor.Value);
                    // E indica que o antecessor dele é o nó vertex
                    nodeSet.FindByValue(neighbor.Value).prevNode = nodeSet.FindByValue(vertex);
                }
            }
        }

        // Elaboração do caminho
        // A partir do vértice que eu estou procurando, volto através da informação prevNode até chegar no nó inicial (start)
        var target = searchFor;

        while(!target.Equals(start))
        {
            // Vou adicionando o nó ao conjunto de nós do caminho
            path.Add(target);
            //Debug.Log(target);
            //Debug.Log(nodeSet.FindByValue(target).prevNode.Value);
            target = nodeSet.FindByValue(target).prevNode.Value;
        }

        // Adiciono o nó inicial
        path.Add(start);

        return path;
        //return visited;
    }

    /// <summary>
    /// Função que retorna o custo da aresta entre os dois nós do grafo
    /// </summary>
    /// <param name="start"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public int GetGraphCost(T start, T next)
    {
        int index = nodeSet.FindByValue(start).Neighbors.IndexOf(nodeSet.FindByValue(next));
        return nodeSet.FindByValue(start).Costs[index];
    }
}
