using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public static int slotsListSize = 1;

    public static string dataPath;
    public static string slotsDataPath;
    public static SlotsList list;
    public static Player player = new Player();
    // Para indicar o slot selecionado (usado no load)
    public static int selectedSlot;

    public void Awake()
    {
        dataPath = Application.persistentDataPath;
        slotsDataPath = System.IO.Path.Combine(dataPath, "listaDeSlots.json");
        list = SlotsListManager.StartList(slotsDataPath);
    }

    #region Save&Load

    public static void Save()
    {
        string nomeDoJson = string.Concat(player.slot, ".json");
        string newDataPath = System.IO.Path.Combine(dataPath, nomeDoJson);
        SaveData.SavePlayerData(newDataPath);
    }

    /// <summary>
    /// Recebe um inteiro informando o slot a ser carregado
    /// </summary>
    /// <param name="i"></param>
    public static void Load(int i)
    {
        string tempPath = i.ToString();
        SaveData.LoadPlayerData(tempPath + ".json");
    }

    #endregion    
}
