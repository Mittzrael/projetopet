using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Classe básica para o controle dos menus que possuem grid
/// </summary>
public class MenuManager : MonoBehaviour, IDeselectHandler
{
    public Transform contentPanel;
    public GameObject itemPrefab;
    protected GameObject itemInstance;
    protected int listSize;

    protected static Button continueButton;
    public static InputField playerName;

    // Para testes
    public Player player;

    void Start()
    {
        ShowItens(listSize);
        continueButton = GameObject.FindGameObjectWithTag("ReadyToGoButton").GetComponent<Button>();
        
        player = SaveManager.instance.player;
    }

    /// <summary>
    /// Instancia os prefabs dos itens que deverão ser exibidos. Quantidade determinada pelos "filhos"
    /// </summary>
    /// <param name="listSize"></param>
    private void ShowItens(int listSize)
    {
        for (int i = 0; i < listSize; i++)
        {
            itemInstance = Instantiate(itemPrefab, contentPanel);
            DisplayItemInfo(i);
        }
    }

    /// <summary>
    /// Mostra as informações dos itens nas prefabs instanciadas
    /// Cada "filho" possui a sua própria função DisplayItemInfo que substitui esta (permitido através do virtual e override)
    /// </summary>
    public virtual void DisplayItemInfo(int i) { }
    
    /// <summary>
    /// Armazena o nome escrito no player do SaveManager para posterior informação
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDeselect(BaseEventData eventData)
    {
        SaveManager.instance.player.nome = playerName.text;
        ReadyToGo();
    }

    /// <summary>
    /// Verifica se pode liberar o botão para continuar
    /// </summary>
    public static void ReadyToGo()
    {
        if (SaveManager.instance.player.avatarSelecionado != -1 && SaveManager.instance.player.nome != "")
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }
}
