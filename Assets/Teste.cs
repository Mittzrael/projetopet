using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Teste : MonoBehaviour
{
    public void Awake()
    {
        SaveManager saveManager;
        saveManager = SaveManager.instance;
        saveManager.player.lastMeal = System.DateTime.UtcNow.ToString();
        TimeManager.instance.PeriodChecker();
    }
}
