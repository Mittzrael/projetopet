using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SlotsListManager : MonoBehaviour {

    #region Save&Load
    public static void SaveSlotsList(string path, SlotsList list)
    {
        string json = JsonUtility.ToJson(list);

        StreamWriter sw = File.CreateText(path);
        sw.Close();

        File.WriteAllText(path, json);
    }

    public static SlotsList LoadSlotsList(string path)
    {
        string json = File.ReadAllText(path);

        return JsonUtility.FromJson<SlotsList>(json);
    }

    #endregion

    /// <summary>
    /// Checa se o número n está dentro da lista dada, caso esteja, retorna true
    /// </summary>
    /// <param name="n"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool CheckSameNumber(int n, SlotsList list)
    {
        foreach (int p in list.slotsList)
        {
            if (n == p)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Caso o número já esteja na lista, retira esse número da lista e salva a nova lista
    /// </summary>
    /// <param name="n"></param>
    public static void RetiraKey(int n)
    {
        if (CheckSameNumber(n, SaveManager.list))
        {
            SaveManager.list.slotsList.Remove(n);
            SaveSlotsList(SaveManager.slotsDataPath, SaveManager.list);
        }
    }

    /// <summary>
    /// Verifica se existe algum número disponível na lista, caso exista, devolve o primeiro número da lista
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static int SlotGiver(SlotsList list)
    {
        if (list.slotsList.Count == 0)
        {
            return -1;
        }

        else
        {
            list.slotsList.Sort();
            return list.slotsList[0];
        }
    }
    
    /// <summary>
    /// Verifica se há o arquivo das lista de slots na pasta destino, caso não exista, cria um de acordo com o tamanho definido no slotsListSize.
    /// </summary>
    /// <param name="path"></param>
    /// <returns>Retorna a lista de slots que será utilizada</returns>
    public static SlotsList StartList(string path)
    {
        SlotsList list = new SlotsList();

        if (System.IO.File.Exists(path))
        {
            list = SlotsListManager.LoadSlotsList(path);
            return list;
        }

        else
        {
            for (int i = 0; i<SaveManager.slotsListSize; i++)
            {
                list.slotsList.Add(i);
            }

            SaveSlotsList(path, list);

            return list;
        }
    }

    /// <summary>
    /// Utilizado quando um save de um jogador é deletado
    /// </summary>
    /// <param name="n"></param>
    public static void ReturnSlot(int n)
    {
        SaveManager.list.slotsList.Add(n);
        SaveSlotsList(SaveManager.slotsDataPath, SaveManager.list);
    }

}
