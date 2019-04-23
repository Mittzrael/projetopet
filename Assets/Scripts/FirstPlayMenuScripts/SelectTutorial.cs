using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTutorial : MonoBehaviour
{
    private int petSelecionado;
    private int residenciaSelecionada;
    public int qtyAnimal;
    public int qtyHome;
    public string[,] listOfTutorials;
    public int[,] listOfGraphs;

    private void Start()
    {
        listOfTutorials = new string[qtyHome, qtyAnimal];
        listOfGraphs = new int[qtyHome, qtyAnimal];
        CompleteArray();
    }

    /// <summary>
    /// Lista de equivalência entre o pet escolhido e residência escolhida para qual scene ele mandar, preencher na mão.
    /// </summary>
    private void CompleteArray()
    {
        ///Valor casa-pet
        listOfTutorials[0, 0] = "Instrucoes1"; ///Cão e casa
        listOfTutorials[0, 1] = "Instrucoes1"; ///Cão de rodas e casa
        listOfTutorials[0, 2] = "Instrucoes1"; ///Gato e casa
        listOfGraphs[0, 0] = 0;
    }

    /// <summary>
    /// Puxa do save qual é o pet e a residência que o jogador escolheu. Caso essa combinação não esteja na lista, emite um erro, caso esteja, passa para a scene que foi relacionada na função "CompleteArray"
    /// </summary>
    public void ChooseTutorial()
    {
        petSelecionado = SaveManager.instance.player.petSelecionado;
        residenciaSelecionada = SaveManager.instance.player.residenciaSelecionada;

        if (listOfTutorials[residenciaSelecionada, petSelecionado] == null)
        {
            Debug.LogError("A combinação residência-pet não existe na lista de strings");
        }
        else
        {
            SaveManager.instance.player.graphNumber = listOfGraphs[residenciaSelecionada, petSelecionado];
            GameManager.instance.LoadSceneWithFade(listOfTutorials[residenciaSelecionada, petSelecionado]);
        }
    }
}
