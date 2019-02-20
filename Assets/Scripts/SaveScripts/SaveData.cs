using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class SaveData: MonoBehaviour {

    private Player newPlayer = new Player();

    public static void SavePlayerData(string path)
    {
        if(SaveManager.player.slot == -1) //O número -1 é dado caso não haja mais espaços livres para salvar o jogo.
        {
            Debug.Log("Não foi possível criar um save");
        }

        else
        {
            SlotsListManager.RetiraKey(SaveManager.player.slot);
            string json = JsonUtility.ToJson(SaveManager.player);

            StreamWriter sw = File.CreateText(path);
            sw.Close();

            File.WriteAllText(path, json);
        }
    }

    /// <summary>
    /// Recebe o nome do arquivo que deve ser carregado
    /// </summary>
    /// <param name="path"></param>
    public static void LoadPlayerData(string path)
    {
        // Completa o caminho com o nome do arquivo
        string dataPath = Path.Combine(SaveManager.dataPath, path);
        // Verifica se o arquivo existe
        if (File.Exists(dataPath))
        {
            // Se existe, carrega o arquivo no SaveManager
            string dataAsJson = File.ReadAllText(dataPath);
            SaveManager.player = JsonUtility.FromJson<Player>(dataAsJson);
        }
        // Se não existe, avisa
        else
        {
            Debug.LogError("Não foi possível carregar o save!");
        }
    }

    public void CreateNewPlayer()
    {
        SaveManager.player = newPlayer;
        SaveManager.player.nome = GameObject.Find("NomeText").GetComponent<Text>().text;
    }

    public static void DeletePlayer()
    {
        string stringSlot = (SaveManager.selectedSlot.ToString() + ".json");

        System.IO.File.Delete(System.IO.Path.Combine(SaveManager.dataPath, stringSlot));
        //Debug.Log(SaveManager.selectedSlot);
        SlotsListManager.ReturnSlot(SaveManager.selectedSlot);
        
    } 
}
