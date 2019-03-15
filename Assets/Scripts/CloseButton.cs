using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    GameObject closeMenu;

    private void Awake()
    {
        closeMenu = Resources.Load("Prefabs/SubMenus/CloseMenu") as GameObject;
    }

    public void OpenMenu()
    {
        if (GameObject.Find("CloseMenu(Clone)") == null)
        {
            GameObject newOptionsMenu;
            newOptionsMenu = Instantiate(closeMenu);
            newOptionsMenu.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }
    }

    public void CloseMenu()
    {
        GameObject buttonParent;
        buttonParent = this.transform.parent.gameObject;
        Destroy(buttonParent);
    }

    public void CarregarCena(string scene)
    {
        GameManager.instance.LoadSceneWithFade(scene);
    }

    public void VoltarMenuInicial()
    {
        GameObject dog = GameObject.Find("dog_mitza");
        if (dog != null)
        {
            Destroy(dog);
        }        
        GameManager.instance.LoadSceneWithFade("Scene01");
    }

    public void FecharJogo()
    {
        Application.Quit();
    }
}