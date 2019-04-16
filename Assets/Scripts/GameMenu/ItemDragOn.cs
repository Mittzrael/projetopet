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
    public GameObject item;
    GameObject scrollViewInventory;
    GameObject panelDragItems;
    GameObject panelInventory;
    public GameObject slot;
    private float distance = 10;

    public string[] allowedDropAreas;
    string currentDropArea;

    void Start()
    {
        scrollViewInventory = GameObject.FindGameObjectWithTag("SVInventario");
        panelDragItems = GameObject.FindGameObjectWithTag("PanelDragItems");
        panelInventory = GameObject.FindGameObjectWithTag("PanelInventory");
    }
    void OnMouseDown()
    {
        GameManager gameManager = GameManager.instance;
        //Desabilita o swipe do inventário
        gameManager.BlockSwipe = true;
        scrollViewInventory.GetComponent<ScrollRect>().enabled = false;
        //Desativa o menu inventário
        transform.parent = panelDragItems.transform;
        panelInventory.SetActive(false);
        GameMenu.isInventoryOpen = false;

        //Drag
        OnMouseDrag();
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    public void OnMouseUp ()
    {
        //Reabilita o swipe do inventário
        GameManager gameManager = GameManager.instance;
        gameManager.BlockSwipe = false;
        
        currentDropArea = DropArea.GetCurrentDropArea();

        //Verifica se area atual é permitida para o item
        foreach (string allowedDropArea in allowedDropAreas)
        {
            if (allowedDropArea == currentDropArea)
            {
                //Destroi o ícone do item
                Destroy(transform.gameObject);
                Destroy(slot);

                //Instancia o item
                Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickedPosition.z = item.transform.position.z;
                Instantiate(item, clickedPosition, Quaternion.identity);

                scrollViewInventory.GetComponent<ScrollRect>().enabled = true;
                return;
            }
        }
        //Caso item tenha sido colocado em área não permitida
        //Retorna item ao inventário,
        this.transform.position = new Vector3(slot.transform.position.x, slot.transform.position.y, -1);
        transform.parent = slot.transform;
        //Ativa o menu inventário
        panelInventory.SetActive(true);
        GameMenu.isInventoryOpen = true;
        scrollViewInventory.GetComponent<ScrollRect>().enabled = true;
    }
}
