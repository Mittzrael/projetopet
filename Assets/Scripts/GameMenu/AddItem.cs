using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    public GameObject itemPrefab1;
    public GameObject itemPrefab2;
    public GameObject inventoryList;
    GameObject item;
    int count = 0;

    public void OnClickBtnAddItem()
    {
        if (count%2 == 0)
        item = Instantiate(itemPrefab1);
        else
        item = Instantiate(itemPrefab2);
        count++;

        item.transform.SetParent(inventoryList.transform, false);
        item.transform.position = new Vector3 (item.transform.position.x, item.transform.position.y, -1);
    }
}