using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    bool salvar = true;

    public void PermitirComida()
    {
        SaveManager.instance.player.health.PutInCleanFoodPot(true);
    }

    public void SaveLoad()
    {
        if (salvar)
        {
            SaveManager.instance.Save();
            GetComponentInChildren<Text>().text = "Carregar";
            salvar = false;
        }

        else
        {
            SaveManager.instance.Load(0);
            GetComponentInChildren<Text>().text = "Salvar";
            salvar = true;
        }
    }
}
