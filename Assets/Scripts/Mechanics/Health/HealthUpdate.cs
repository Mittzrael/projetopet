﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe para ser colocada nos objetos que serão atualizados de acordo com o valor do dado de saúde no SaveManager.
/// </summary>
public class HealthUpdate : MonoBehaviour
{
    private SaveManager saveManager;
    private float fillAmount;
    [SerializeField]
    private float timeToDecrease;

    public void Start()
    {
        saveManager = SaveManager.instance;
        Invoke(gameObject.name, 0);
    }



    #region Atualização constante das barras
    private void Update()
    {
        Invoke(gameObject.name, 0);
    }
    
    
    private void Ration()
    {
        if (saveManager.player.health.GetCleanFoodPot())
        {
            transform.GetChild(1).GetComponent<Image>().fillAmount = 0;// saveManager.player.health.GetHungry();
        }
        else
        {
            transform.GetChild(1).GetComponent<Image>().fillAmount = 1;// saveManager.player.health.GetHungry();
        }
    }
    

    private void Water()
    {
        //transform.GetChild(1).GetComponent<Image>().fillAmount = saveManager.player.health.GetThirsty();
    }

    private void Clean()
    {
        transform.GetChild(1).GetComponent<Image>().fillAmount = saveManager.player.health.GetHygiene();
    }

    private void Happiness()
    {
        transform.GetChild(1).GetComponent<Image>().fillAmount = saveManager.player.health.GetHappiness();
    }
    #endregion
}
