using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpdate : MonoBehaviour
{
    private SaveManager saveManager;
    private float fillAmount;

    public void Start()
    {
        saveManager = SaveManager.instance;
        Invoke(gameObject.name, 0);
    }

    private void Update()
    {
        Invoke(gameObject.name, 0);
    }

    private void Ration()
    {
        transform.GetChild(1).GetComponent<Image>().fillAmount = saveManager.player.health.GetHungry();
    }

    private void Water()
    {
        transform.GetChild(1).GetComponent<Image>().fillAmount = saveManager.player.health.GetThirst();
    }

    private void Clean()
    {
        transform.GetChild(1).GetComponent<Image>().fillAmount = saveManager.player.health.GetHygiene();
    }
}
