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

    public void CarregarCena()
    {
        GameManager.instance.LoadSceneWithFade("Scene01");
    }

    public void FecharJogo()
    {
        Application.Quit();
    }
}
