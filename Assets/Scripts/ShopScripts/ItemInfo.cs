using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que guarda as informações de um item que pode ser vendido na loja do jogo
/// </summary>
[Serializable]
public class ItemInfo {

    public string itemInfo;
    public int itemPrice = 200;
    public Sprite itemSprite;
    public Color itemColor;
    public bool availableToBuy = true;
}
