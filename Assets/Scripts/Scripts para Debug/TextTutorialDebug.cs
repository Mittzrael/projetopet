using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTutorialDebug : MonoBehaviour
{
    private void Update()
    {
        GetComponentInChildren<Text>().text = (SaveManager.instance.player.waterPotLocation.ToString() + SaveManager.instance.player.foodPotLocation.ToString());
    }
}
