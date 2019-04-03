using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonController", menuName = "My Assets/Button Controller")]
public class ButtonController : ScriptableObject
{
    SaveManager saveManager;

    /// <summary>
    /// Função para permitir que um botão acesse o load scene (que está no GameManager)
    /// </summary>
    /// <param name="scene"></param>
    public void ChangeScene(string scene)
    {
        GameManager.instance.LoadSceneWithFade(scene);
    }

    public void CallSave()
    {
        saveManager = SaveManager.instance;
        saveManager.Save();
    }

    public void CallLoad()
    {
        saveManager = SaveManager.instance;
        saveManager.Load(0); //No caso de só permitir um save, mudar se houver necessidade
    }

    public void CallNewPlayer()
    {
        saveManager = SaveManager.instance;
        saveManager.CreateNewPlayer();
    }

    public void CallResetSave()
    {
        saveManager = SaveManager.instance;
        saveManager.ResetSave();
    }

    #region PARA TESTES =D
    public void BeHappy()
    {
        SaveManager.instance.player.health.PutInHappiness(10f);
    }

    public void PetHungry()
    {
        SaveManager.instance.player.health.PutInHungry(-10f);
    }

    public void PetThirsty()
    {
        SaveManager.instance.player.health.PutInThirsty(-10f);
    }

    public void PetPee()
    {
        SaveManager.instance.player.health.PutInPee(10f);
    }

    public void PetPoop()
    {
        SaveManager.instance.player.health.PutInPoop(10f);
    }

    public void PetIsEducated()
    {
        if (SaveManager.instance.player.health.GetWhereToPP() == 0)
        {
            SaveManager.instance.player.health.PutInWhereToPP(10f);
        }
        else
        {
            SaveManager.instance.player.health.PutInWhereToPP(-10f);
        }
    }

    #endregion
}