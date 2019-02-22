using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject itemPrefab;
    protected GameObject itemInstance;
    protected int listSize;

    protected static Button continueButton;

    void Start()
    {
        ShowItens(listSize);
        continueButton = GameObject.FindGameObjectWithTag("ReadyToGoButton").GetComponent<Button>();
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

    public static int GridToIndex()
    {
        return 0;
    }

    public static void ReadyToGo()
    {
        continueButton.interactable = true;
    }
}
