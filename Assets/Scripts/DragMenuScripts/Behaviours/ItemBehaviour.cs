using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Classe básica para o comportamento dos itens nos menus que utilizam grid layout
/// </summary>
public class ItemBehaviour : MonoBehaviour, IPointerClickHandler
{
    protected GridLayout gridLayout;
    protected Vector3 wcellPosition;
    protected Vector3Int cellPosition;
    [Tooltip("Índice do objeto no grid (relativo a vetor)")]
    protected int index;

    private void Awake()
    {
        // Pega o componente GridLayout (para facilidades)
        gridLayout = transform.parent.GetComponent<GridLayout>();
    }

    /// <summary>
    /// Pega o índice do item sob o clicke do mouse
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        GetGridPosition();
        GridtoIndex(cellPosition);
        SaveIndexOnPlayer();
        MenuManager.ReadyToGo();
        //Debug.Log(cellPosition + " " + index);
    }

    /// <summary>
    /// Transforma a posição global para a posição no grid
    /// </summary>
    public void GetGridPosition()
    {
        wcellPosition = transform.position;
        cellPosition = gridLayout.WorldToCell(wcellPosition);
    }

    /// <summary>
    /// Transforma a posição do grid em um índice de vetor
    /// </summary>
    /// <param name="pos"></param>
    public virtual void GridtoIndex(Vector3Int pos)
    {
        index = (pos.x/550);
    }

    /// <summary>
    /// Função abstrata para salvar o indice do objeto selecionado (seja ele qual for)
    /// </summary>
    public virtual void SaveIndexOnPlayer() { }
}
