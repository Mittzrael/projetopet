using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Função para jogar em um objeto para poder arrastá-lo
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class InventoryDragOn : MonoBehaviour
{
    GameObject ScrollViewInventory;
    private float distance = 10;

    private void Start()
    {
        ScrollViewInventory = GameObject.FindGameObjectWithTag("SVInventario");
    }
    void OnMouseDown()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.BlockSwipe = true;
        ScrollViewInventory.GetComponent<ScrollRect>().enabled = false;
        OnMouseDrag();
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    void OnMouseUp()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.BlockSwipe = false;
        ScrollViewInventory.GetComponent<ScrollRect>().enabled = true;
        Destroy(transform.parent.gameObject);
    }
}
