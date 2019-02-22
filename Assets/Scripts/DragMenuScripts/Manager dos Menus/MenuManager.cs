using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject itemPrefab;
    protected GameObject itemInstance;
    protected int listSize;

    void Start()
    {
        ShowItens(listSize);
    }

    private void ShowItens(int listSize)
    {
        for (int i = 0; i < listSize; i++)
        {
            itemInstance = Instantiate(itemPrefab, contentPanel);
            DisplayItemInfo(i);
        }
    }

    // Cada "filho" possui a sua própria função DisplayItemInfo que substitui esta (permitido através do virtual e override)
    public virtual void DisplayItemInfo(int i) { }
}
