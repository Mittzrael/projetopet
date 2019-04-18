using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOpen: MonoBehaviour
{
    public GameObject menuInventario;
    public static bool isInventoryOpen = false;

    //Abre o inventário
    public void OnClickOpenInventory()
    {
        if (!isInventoryOpen)
        {
            menuInventario.SetActive(true);
            isInventoryOpen = true;
        }
        else
        {
            menuInventario.SetActive(false);
            isInventoryOpen = false;
        }
    }
}