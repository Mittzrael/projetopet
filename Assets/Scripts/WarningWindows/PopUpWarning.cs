using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWarning : MonoBehaviour
{
    public static PopUpWarning instance;
    // Lista contendo os avisos que devem aparecer - original está no pet
    private WarningsList[] warningsList;

    [Tooltip("Lista contento os objetos da scene que serão controlados (ativados/desativados) - Completada via script")]
    public List<string> warnings;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Pega todos os objetos Warnings e posiciona na lista
        for (int i = 0; i < transform.childCount; i++)
        {
            warnings.Add(transform.GetChild(i).name);
        }

        // Pega as listas de aviso que estão armazenadas no pet
        warningsList = GameObject.FindGameObjectWithTag("PetFather").GetComponentInChildren<Pet>().warningsLists;

        // Chama todos os avisos que estavam ativos quando o jogador parou de jogar na última vez
        CallSavedWarnings();
    }

    /// <summary>
    /// Função que verifica quais avisos estavam salvos (não foram resolvidos na ultima vez) e os ativa
    /// </summary>
    public void CallSavedWarnings()
    {
        foreach (string name in SaveManager.instance.player.savedWarnings)
        {
            CallWarning(name);
        }
    }

    /// <summary>
    /// Chama todos os avisos existentes na lista de avisos (passada pelo índice da lista)
    /// </summary>
    /// <param name="listIndex"></param>
    public void CallAllWarnings(int listIndex)
    {
        for (int i = 0; i < warningsList[listIndex].warnings.Length; i++)
        {
            string name = warningsList[listIndex].warnings[i].warningName;
            // Se o aviso não estiver na lista de avisos salvas
            if (!SaveManager.instance.player.savedWarnings.Contains(name))
            {
                // Adiciona e ativa o aviso
                SaveManager.instance.player.savedWarnings.Add(name);
                CallWarning(name);
            }
        }
    }

    /// <summary>
    /// Resolve todos os avisos (remove da tela e da lista)
    /// </summary>
    /// <param name="listIndex"></param>
    public void SolveAllWarnings(int listIndex)
    {
        for (int i = 0; i < warningsList[listIndex].warnings.Length; i++)
        {
            SolveWarning(warningsList[listIndex].warnings[i].warningName);
            SaveManager.instance.player.savedWarnings.Clear();
        }
    }

    /// <summary>
    /// Chama um aviso específico da lista de avisos
    /// </summary>
    /// <param name="warningName"></param>
    public void CallWarning(string warningName)
    {
        transform.GetChild(warnings.IndexOf(warningName)).gameObject.SetActive(true);
    }

    /// <summary>
    /// Resolve um aviso específico da lista de avisos
    /// </summary>
    /// <param name="warningName"></param>
    public void SolveWarning(string warningName)
    {
        transform.GetChild(warnings.IndexOf(warningName)).gameObject.SetActive(false);
        SaveManager.instance.player.savedWarnings.Remove(warningName);
    }
}