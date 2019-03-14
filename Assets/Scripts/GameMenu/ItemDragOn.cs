using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script para instanciação de itens através do menu inventário
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class ItemDragOn : MonoBehaviour
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
        //Desabilita o swipe do inventário
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
        //Reabilita o swipe do inventário
        gameManager.BlockSwipe = false;
        ScrollViewInventory.GetComponent<ScrollRect>().enabled = true;
        //Destroi o ícone do item
        Destroy(transform.gameObject);
        //Instancia o item
        Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickedPosition.z = 0;
        //Instantiate(item, clickedPosition, Quaternion.identity);

    }
}
