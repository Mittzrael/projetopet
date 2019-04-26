using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    public void PermitirComida()
    {
        SaveManager.instance.player.health.PutInCleanFoodPot(true);
    }
}
