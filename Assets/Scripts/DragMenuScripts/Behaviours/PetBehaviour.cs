using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetBehaviour : ItemBehaviour
{
    public void Start()
    {
        gridOffset = 2;
    }

    public override void SaveIndexOnPlayer()
    {   
        SaveManager.instance.player.petSelecionado = index;
    }
}
