using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemBehaviour : MonoBehaviour, IPointerClickHandler
{
    protected GridLayout gridLayout;
    protected Vector3 wcellPosition;
    protected Vector3Int cellPosition;
    protected int index;
    protected int gridOffset;

    private void Awake()
    {
        gridLayout = transform.parent.GetComponent<GridLayout>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetGridPosition();
        GridtoIndex(cellPosition, gridOffset);
        SaveIndexOnPlayer();
        MenuManager.ReadyToGo();
        //Debug.Log(cellPosition + " " + index);
    }

    public void GetGridPosition()
    {
        wcellPosition = transform.position;
        cellPosition = gridLayout.WorldToCell(wcellPosition);
    }

    public virtual void GridtoIndex(Vector3Int pos, int gridOffset)
    {
        index = (pos.x/550 + gridOffset);
    }

    public virtual void SaveIndexOnPlayer() { }
}
