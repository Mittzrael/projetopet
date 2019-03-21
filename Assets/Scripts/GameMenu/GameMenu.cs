using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script para o funcionamento geral do menu
/// </summary>
public class GameMenu : MonoBehaviour
{
    public GameObject btnInventario, btnPetShop, btnAcaoPet;
    public GameObject panelInventario, panelAcao;
    bool isMenuOpen = false;
    public static bool isInventoryOpen = false;
    bool isActionOpen = false;

    //Click no botão menu
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
            panelAcao.SetActive(false);
            isMenuOpen = false;
            isInventoryOpen = false;
            isActionOpen = false;
        }
    }

    //Click no botão inventário
    public void OnClickBtnInventario()
    {
        if (!isInventoryOpen)
        {
            panelInventario.SetActive(true);
            panelAcao.SetActive(false);
            isInventoryOpen = true;
            isActionOpen = false;
        }
        else
        {
            panelInventario.SetActive(false);
            isInventoryOpen = false;
        }
    }

    //Click no botão Ações
    public void OnClickBtnAcoes()
    {
        if (!isActionOpen)
        {
            panelAcao.SetActive(true);
            panelInventario.SetActive(false);
            isActionOpen = true;
            isInventoryOpen = false;
        }
        else
        {
            panelAcao.SetActive(false);
            isActionOpen = false;
        }
    }

    //Click no botão PetShop
    public void OnClickBtnPetShop()
    {
        Debug.Log("Vai para o pet shop");
    }

    //Click fora do menu (fecha menu)
    public void OnClickGameArea()
    {
        if (isMenuOpen)
            OnClickBtnMenu();
    }
}