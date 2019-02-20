using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmMenu : MonoBehaviour {

    #region Variáveis Globais
    private ShopManager shopManager;
    #endregion

    private void Start()
    {
        // Encontra a referencia do ShopManager
        shopManager = FindObjectOfType<ShopManager>();
    }

    /// <summary>
    /// Confirma a compra do item escolhido
    /// </summary>
    public void ConfirmBuy()
    {
        shopManager.ConfirmTrade();
    }
}
