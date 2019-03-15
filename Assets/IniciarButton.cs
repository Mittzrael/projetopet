using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IniciarButton : MonoBehaviour
{
    private SaveManager saveManager;
    private bool newGame = false;

    private void Awake()
    {
        saveManager = SaveManager.instance;
        saveManager.Load(0);
        if (saveManager.player.slot != -1)
        {
            transform.GetComponentInChildren<Text>().text = "Continuar";
            newGame = false;
        }
        else
        {
            newGame = true;
        }
    }

    public void OnClick()
    {
        if (newGame)
        {
            GameManager.instance.LoadSceneWithFade("Scene02");
        }
        else
        {
            GameManager.instance.LoadSceneWithFade("Ambiente");
        }
    }
}
