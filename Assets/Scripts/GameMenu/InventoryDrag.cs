using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDrag : MonoBehaviour
{
    public GameObject item;

    public void OnItemMousedown()
    {
        Debug.Log("oi");
        //Instancia o itemDrag
        Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickedPosition.z = 0;
        Instantiate(item, clickedPosition, Quaternion.identity);
    }
}
