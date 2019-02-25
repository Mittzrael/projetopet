using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetBehaviour : ItemBehaviour
{
    /// <summary>
    /// Salva o indice do objeto selecionado como pet
    /// </summary>
    public override void SaveIndexOnPlayer()
    {
        SaveManager.player.petSelecionado = index;
    }
}
