using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColocaPoteNoLugar : MonoBehaviour
{
    public void ColocaPote()
    {       
        SaveManager.instance.player.foodPotLocation.elementName = "Pote de Comida";
        SaveManager.instance.player.foodPotLocation.elementPosition = new Vector3(1500f, -410f, 5f);
        SaveManager.instance.player.foodPotLocation.sceneName = "MainRoom";

        SaveManager.instance.player.waterPotLocation.elementName = "Pote de Água";
        SaveManager.instance.player.waterPotLocation.elementPosition = new Vector3(-300f, -356f, 5f);
        SaveManager.instance.player.waterPotLocation.sceneName = "Yard(1)";

        GetComponentInChildren<Text>().text = "Colocou";
    }
}
