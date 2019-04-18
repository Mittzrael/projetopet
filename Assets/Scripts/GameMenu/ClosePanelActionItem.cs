using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanelActionItem : MonoBehaviour
{
    public GameObject menuInventory;
    GameObject panelInventory;
    GameObject panelActionsItems;

    void Start()
    {
        panelInventory = GameObject.FindGameObjectWithTag("PanelInventory");
        panelActionsItems = GameObject.FindGameObjectWithTag("PanelActionsItems");
    }

    public void OnClickArea()
    {
        foreach (Transform child in panelActionsItems.transform)
        {
            child.gameObject.SetActive(false);
        }
        panelInventory.SetActive(true);
        InventoryOpen.isInventoryOpen = false;
        menuInventory.SetActive(false);
    }
}