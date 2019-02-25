using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject btnInventario, btnPetShop, btnAcaoPet;
    bool isMenuOpen = false;

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
            isMenuOpen = false;
        }
    }

}