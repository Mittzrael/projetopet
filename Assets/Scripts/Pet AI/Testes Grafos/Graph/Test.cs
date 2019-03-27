using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GraphCreator petAccess;
    public Graph<string> petAccessG = new Graph<string>();

    // Start is called before the first frame update
    void Start()
    {
        #region Teste Grafo
        /*
        Graph<string> web = new Graph<string>();

        web.AddNode("Privacy.htm");
        web.AddNode("People.aspx");
        web.AddNode("About.htm");
        web.AddNode("Index.htm");
        web.AddNode("Products.aspx");
        web.AddNode("Contact.aspx");

        web.AddDirectedEdge("People.aspx", "Privacy.htm");  // People -> Privacy

        web.AddDirectedEdge("Privacy.htm", "Index.htm");    // Privacy -> Index
        web.AddDirectedEdge("Privacy.htm", "About.htm");    // Privacy -> About

        web.AddDirectedEdge("About.htm", "Privacy.htm");    // About -> Privacy
        web.AddDirectedEdge("About.htm", "People.aspx");    // About -> People
        web.AddDirectedEdge("About.htm", "Contact.aspx");   // About -> Contact

        web.AddDirectedEdge("Index.htm", "About.htm");      // Index -> About
        web.AddDirectedEdge("Index.htm", "Contact.aspx");   // Index -> Contacts
        web.AddDirectedEdge("Index.htm", "Products.aspx");  // Index -> Products

        web.AddDirectedEdge("Products.aspx", "Index.htm");  // Products -> Index
        web.AddDirectedEdge("Products.aspx", "People.aspx");// Products -> People


        var path = web.BFS("About.htm", "People.aspx");
        Debug.Log(string.Join(" <- ", path));
        path = web.BFS("About.htm", "Products.aspx");
        Debug.Log(string.Join(" <- ", path));
        string pathS = string.Join(",", path);
        Debug.Log(pathS.Split(',')[0]);
        */
        #endregion

        #region Teste ProjectPet

        //Graph<string> petAccess = new Graph<string>();

        //GraphNode<string> kitchen = new GraphNode<string>("Kitchen");
        //GraphNode<string> yard = new GraphNode<string>("Yard");
        //GraphNode<string> livingRoom = new GraphNode<string>("LivingRoom");

        //petAccess.AddNode(kitchen);
        //petAccess.AddNode(yard);
        //petAccess.AddNode(livingRoom);

        //petAccess.AddDirectedEdge(livingRoom, yard, -2723);
        //petAccess.AddDirectedEdge(livingRoom, kitchen, 2708);

        //petAccess.AddDirectedEdge(kitchen, livingRoom, 812);

        //petAccess.AddDirectedEdge(yard, livingRoom, 2431);

        //Debug.LogWarning("Caminho do quintal até a cozinha");
        //var path = petAccess.BFS(yard.Value, kitchen.Value);
        //string pathS = HasHSetToString(path);
        ////PrintPath(pathS.Split(','));
        //string[] name = pathS.Split(',');
        //for (int i = name.Length - 1; i > 0; i--)
        //{
        //    Debug.Log(name[i] + " -> " + petAccess.teste(name[i], name[i - 1]));
        //}

        /*
        Debug.LogWarning("Caminho da sala até a cozinha");
        path = petAccess.BFS(livingRoom.Value, kitchen.Value);
        pathS = HasHSetToString(path);
        PrintPath(pathS.Split(','));

        Debug.LogWarning("Caminho do quintal até a sala");
        path = petAccess.BFS(yard.Value, livingRoom.Value);
        pathS = HasHSetToString(path);
        PrintPath(pathS.Split(','));
        */
        #endregion

        petAccess.CreateGraph();
        var path = petAccess.petAccessGraph.BFS("Yard", "Kitchen");
        string pathS = HasHSetToString(path);
        //PrintPath(pathS.Split(','));
        string[] name = pathS.Split(',');
        for (int i = name.Length - 1; i > 0; i--)
        {
            Debug.Log(name[i] + " -> " + petAccess.petAccessGraph.teste(name[i], name[i - 1]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string HasHSetToString(HashSet<string> hashSet)
    {
        return string.Join(",", hashSet);
    }
    
    public void PrintPath(string[] path)
    {
        for (int i = path.Length - 1; i >= 0; i--)
        {
            Debug.Log(path[i]);
        }
    }
}
