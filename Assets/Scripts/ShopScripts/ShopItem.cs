using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

    #region Variáveis Globais
    public ItemInfo itemInfo;
    private ShopManager shopManager;
    #endregion

    private void Start()
    {
        // Atualiza a informação do prefab com a do item a ser mostrado
        transform.GetChild(0).GetComponent<Text>().text = itemInfo.itemInfo;
        transform.GetChild(1).GetComponent<Image>().sprite = itemInfo.itemSprite;
        transform.GetChild(1).GetComponent<Image>().color = itemInfo.itemColor;
        transform.GetChild(3).GetComponent<Text>().text = itemInfo.itemPrice.ToString();
        // Encontra a referencia do ShopManager para poder chamar as funçãoe necessárias
        shopManager = GameObject.FindObjectOfType<ShopManager>();
    }

    /// <summary>
    /// Função do botão de compra do item
    /// </summary>
    public void BuyButton()
    {
        // Encontra o índice do item escolhido e informa o ShopManager
        shopManager.chosenItem = GridToIndex();
        // Chama o menu de confirmação de compra
        shopManager.CallConfirmMenu();
    }
    
    /// <summary>
    /// Transforma a posição do botao selecionado do grid em um índice de vetor
    /// </summary>
    /// <returns></returns>
    private int GridToIndex()
    {
        GridLayout gridLayout = transform.parent.GetComponent<GridLayout>();
        Vector3 wcellPosition = transform.position;
        Vector3Int cellPosition = gridLayout.WorldToCell(wcellPosition);
        return ((cellPosition.x - 40) / 70 - 3 * ((cellPosition.y + 55) / 100));
    }

    /// <summary>
    /// Atualiza a prefab do item para a versão pós-compra
    /// </summary>
    public void ItemSold()
    {
        this.transform.GetChild(5).GetComponent<Button>().interactable = false;
        this.transform.GetChild(4).GetComponent<Image>().enabled = true;
    }
}
