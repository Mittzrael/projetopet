using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject btnInventario, btnPetShop, btnAcaoPet;
    public GameObject panelInventario;
    bool isMenuOpen = false;
    bool isInventoryOpen = false;

    public void OnClickBtnMenu()
    {
        if (!isMenuOpen)
        {
            btnInventario.SetActive(true);
            btnPetShop.SetActive(true);
            btnAcaoPet.SetActive(true);
            isMenuOpen = true;
        }
        else
        {
            btnInventario.SetActive(false);
            btnPetShop.SetActive(false);
            btnAcaoPet.SetActive(false);
            panelInventario.SetActive(false);
            isMenuOpen = false;
        }
    }

    public void OnClickBtnInventario()
    {
        if (!isInventoryOpen)
        {
            panelInventario.SetActive(true);
            isInventoryOpen = true;
        }
        else
        {
            panelInventario.SetActive(false);
            isInventoryOpen = false;
        }
    }
}