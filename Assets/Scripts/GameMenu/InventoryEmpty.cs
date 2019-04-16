using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryEmpty : MonoBehaviour
{
    public GameObject inventoryList;
    public GameObject textInventarioVazio;

    //Verifica de inventário está vazio e exibe mensagem
    void Update()
    {
        if (inventoryList.transform.childCount == 0)
        {
            textInventarioVazio.SetActive(true);
        }
        else
        {
            textInventarioVazio.SetActive(false);
        }
    }
}