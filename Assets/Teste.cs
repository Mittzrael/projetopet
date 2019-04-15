using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Teste : MonoBehaviour
{
    public void Start()
    {
        SaveManager saveManager;
        saveManager = SaveManager.instance;
        //saveManager.player.timeHelper.lastMeal = System.DateTime.UtcNow.ToString();
        TimeManager.instance.PeriodChecker();
    }

    public void SetLastMeal()
    {
        SaveManager.instance.player.timeHelper.lastMeal = System.DateTime.UtcNow.ToString();
    }
}
