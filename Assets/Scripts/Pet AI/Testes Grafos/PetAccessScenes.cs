using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PetAccessScenes", menuName = "My Assets/PetAccessScenes")]
public class PetAccessScenes : ScriptableObject
{
    [Tooltip("Nome das scenes que o pet tem acesso")]
    public string[] sceneName;

    public void SetPetHomeInfo()
    {
        SaveManager.instance.player.petAccessScenes = this;
    }
}