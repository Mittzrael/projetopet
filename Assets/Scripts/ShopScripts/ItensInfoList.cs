using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensInfoList : MonoBehaviour {

    [Tooltip("Lista dos avatares vendidos na loja")]
    public ItemInfo[] avatarsToSell;
    [Tooltip("Lista das cores vendidas na loja")]
    public ItemInfo[] colorsToSell;
    [Tooltip("Lista dos extras vendidos na loja")]
    public ItemInfo[] extrasToSell;
}
