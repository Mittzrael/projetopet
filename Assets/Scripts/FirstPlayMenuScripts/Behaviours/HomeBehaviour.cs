using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HomeBehaviour : ItemBehaviour
{
    /// <summary>
    /// Salva o indice do objeto selecionado como residencia
    /// </summary>
    public override void SaveIndexOnPlayer()
    {
        SaveManager.instance.player.residenciaSelecionada = index;
    }
}
