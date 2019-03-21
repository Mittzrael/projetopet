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

    public string[] allowedZones;
    string currentZone;

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
        //Destroi o ícone do item
        Destroy(transform.gameObject);
        Destroy(slot);
        //Ativa o menu inventário
        panelInventory.SetActive(true);
        GameMenu.isInventoryOpen = true;
        scrollViewInventory.GetComponent<ScrollRect>().enabled = true;

        currentZone = PlaceableZone.GetCurrentZone();

        //Verifica se zona atual é permitida para o item
        foreach(string allowedZone in allowedZones)
        {
            if(allowedZone == currentZone)
            {
                //Instancia o item
                Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickedPosition.z = 0;
                Instantiate(item, clickedPosition, Quaternion.identity);
                
                break;
            }
        }
    }
}
