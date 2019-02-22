using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonController", menuName = "My Assets/Button Controller")]
public class ButtonController : ScriptableObject
{
    public void ChangeScene(string scene)
    {
        GameManager.instance.LoadSceneWithFade(scene);
    }
}
