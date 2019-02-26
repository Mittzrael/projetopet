using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HomeBehaviour : ItemBehaviour
{
    public void Start()
    {
        gridOffset = 2;
    }

    public override void SaveIndexOnPlayer()
    {
        SaveManager.instance.player.residenciaSelecionada = index;
    }
}
