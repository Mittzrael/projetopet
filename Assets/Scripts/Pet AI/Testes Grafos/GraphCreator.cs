using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PetAccessGraph", menuName = "My Assets/Grafo dos Locais de Acesso do Pet")]
public class GraphCreator : ScriptableObject
{
    public string[] sceneNames;
    public GraphHelperBuilder[] sceneTransitions;
    public Graph<string> petAccessGraph;
    
    // Start is called before the first frame update
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

[System.Serializable]
public struct GraphHelperBuilder
{
    public string fromScene;
    public string toScene;
    public int doorLocation;
}