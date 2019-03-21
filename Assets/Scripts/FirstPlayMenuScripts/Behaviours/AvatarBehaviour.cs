using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AvatarBehaviour : ItemBehaviour
{
    /// <summary>
    /// Salva o indice do objeto selecionado como avatar
    /// </summary>
    public override void SaveIndexOnPlayer()
    {
        SaveManager.instance.player.avatarSelecionado = index;
    }
}
