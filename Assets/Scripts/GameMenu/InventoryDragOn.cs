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
    GameObject panelInventario, scrollViewInventario;
    private float distance = 10;

    private void Start()
    {
        scrollViewInventario = GameObject.FindGameObjectWithTag("SVInventario");
        panelInventario = GameObject.FindGameObjectWithTag("PanelInventario");
    }
    void OnMouseDown()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.BlockSwipe = true;
        scrollViewInventario.SetActive(false);
        transform.SetParent(panelInventario.transform, false);
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
        scrollViewInventario.SetActive(true);
        Destroy(gameObject);
    }
}
