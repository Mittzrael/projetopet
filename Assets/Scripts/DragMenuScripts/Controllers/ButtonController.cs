using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonController", menuName = "My Assets/Button Controller")]
public class ButtonController : ScriptableObject
{
    /// <summary>
    /// Função para permitir que um botão acesse o load scene (que está no GameManager)
    /// </summary>
    /// <param name="scene"></param>
    public void ChangeScene(string scene)
    {
        GameManager.instance.LoadSceneWithFade(scene);
    }
}
