using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsável pela criação de grafos
/// </summary>
[CreateAssetMenu(fileName = "PetAccessGraph", menuName = "My Assets/Grafo dos Locais de Acesso do Pet")]
public class GraphCreator : ScriptableObject
{
    public string[] sceneNames;
    public GraphHelperBuilder[] sceneTransitions;
    public Graph<string> petAccessGraph;
    
    /// <summary>
    /// Função que cria um grafo do tipo STRING com as informações passadas via INSPECTOR.
    /// Criada para facilitar a criação dos grafos utilizados
    /// </summary>
    /// <returns></returns>
    public Graph<string> CreateGraph()
    {
        petAccessGraph = new Graph<string>();

        for (int i = 0; i < sceneNames.Length; i++)
        {
            petAccessGraph.AddNode(sceneNames[i]);
        }

        for (int i = 0; i < sceneTransitions.Length; i++)
        {
            petAccessGraph.AddDirectedEdge(sceneTransitions[i].fromScene, sceneTransitions[i].toScene, sceneTransitions[i].doorLocation);
        }

        return petAccessGraph;
    }
}

/// <summary>
/// Struct utilizada para facilitar a entrada dos dados dos nós do grafo e dos pesos das arestas
/// </summary>
[System.Serializable]
public struct GraphHelperBuilder
{
    public string fromScene;
    public string toScene;
    public int doorLocation;
}