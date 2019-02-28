using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToCallSave : MonoBehaviour
{
    SaveManager saveManager;

    private void Awake()
    {
        saveManager = SaveManager.instance;
    }

    public void CallSave()
    {
        saveManager.Save();
    }

    public void CallLoad()
    {
        saveManager.Load(0); //No caso de só permitir um save, mudar se houver necessidade
    }

    public void CallNewPlayer()
    {
        saveManager.CreateNewPlayer();
    }
}
