using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private MenuItem[] itensToShow;
    public MenuItemList itemList;

    public Transform contentPanel;
    private GameObject itemInstance;
    public GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        itensToShow = itemList.items;
        ShowItens();
    }

    private void ShowItens()
    {
        Debug.Log(itemList.items.Length);
        for (int i = 0; i < itensToShow.Length; i++)
        {
            itemInstance = Instantiate(itemPrefab, GameObject.Find("AvatarPanel").transform);
            DisplayItemInfo(i);
        }
    }

    private void DisplayItemInfo(int i)
    {

    }
}
