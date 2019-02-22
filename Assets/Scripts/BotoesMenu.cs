using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotoesMenu : MonoBehaviour
{
    GameObject menuOptions;

    void Start()
    {
        menuOptions = Resources.Load("Prefabs/SubMenus/MenuOptions") as GameObject;
    }

    public void OpenOptionsMenu()//abre menu opções menu principal
    {
        if (GameObject.Find("MenuOptions(Clone)") == null)
        {            
            GameObject newOptionsMenu;
            newOptionsMenu = Instantiate(menuOptions);
            newOptionsMenu.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }
    }
}
