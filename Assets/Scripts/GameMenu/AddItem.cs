using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject inventoryList;

    public void OnClickBtnAddItem()
    {
        GameObject item = Instantiate(itemPrefab);
        item.transform.SetParent(inventoryList.transform, false);
    }
}