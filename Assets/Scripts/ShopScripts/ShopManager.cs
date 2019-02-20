using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {

    #region Variáveis Globais
    [Tooltip("Lista dos avatares vendidos na loja")]
    private ItemInfo[] avatarsToSell;
    [Tooltip("Lista das cores vendidas na loja")]
    private ItemInfo[] colorsToSell;
    [Tooltip("Lista dos extras vendidos na loja")]
    private ItemInfo[] extrasToSell;
    [Tooltip("GameObject que contém a lista dos itens do jogo")]
    public ItensInfoList itensInfoList;

    [Tooltip("Prefab genérico do item da loja (aparência)")]
    public GameObject itemPrefab;

    private ShopToggleWindow shopToggleWindow;
    // Utilizado para não perder a referência do prefab
    private GameObject itemPrefabInstace;
    private GameObject playerMoney;

    [Tooltip("Prefab do menu de confirmação de compra")]
    public GameObject confirmMenu;

    private string toggleSelected;
    private int chosenItemPrice;
    // Indicação do índice do item escolhido
    public int chosenItem;
    #endregion

    void Start () {
        // Pega as informações do prefab itensInfoList e joga nos respectivos vetores
        avatarsToSell = itensInfoList.avatarsToSell;
        colorsToSell = itensInfoList.colorsToSell;
        extrasToSell = itensInfoList.extrasToSell;

        // Encontra o painel de seleção e verifica o nome do toggle selecionado
        shopToggleWindow = FindObjectOfType<ShopToggleWindow>();
        toggleSelected = shopToggleWindow.SelectedToggle();

        // Encontra o indicador do dinheiro do jogador e atualiza para o valor que ele possui
        playerMoney = GameObject.Find("PlayerMoney");
        playerMoney.GetComponent<Text>().text = SaveManager.player.dinheiro.ToString();

        // Chama a função de acordo com o nome passado (chama como uma coroutine)
        Invoke(string.Concat(toggleSelected, "ShopWindow"), 0);
	}
	
    /// <summary>
    /// Chama a função ShowItens mostrando os avatares
    /// </summary>
	public void AvatarToggleShopWindow()
    {
        ShowItems(avatarsToSell);
    }

    /// <summary>
    /// Chama a função ShowItens mostrando as cores
    /// </summary>
    public void CoresToggleShopWindow()
    {
        ShowItems(colorsToSell);
    }

    /// <summary>
    /// Chama a função ShowItens mostrando os extras
    /// </summary>
    public void ExtrasToggleShopWindow()
    {
        ShowItems(extrasToSell);
    }

    /// <summary>
    /// Função que mostra os itens passados através de itemsToShow
    /// </summary>
    /// <param name="itemsToShow"></param>
    public void ShowItems(ItemInfo[] itemsToShow)
    {
        // Destroi todos os itens que existiam anteriormente para a exibição dos itens referentes a aba selecionada
        DestroyShopItens();
        // Verifica qual é a aba selecionada
        toggleSelected = shopToggleWindow.SelectedToggle();

        for (int i = 0; i < itemsToShow.Length; i++)
        {
            // Instancia os itens como filhos do GameObject Content (responsável por organizar o grid)
            itemPrefabInstace = Instantiate(itemPrefab, GameObject.Find("Content").transform);
            // Passa as informações do item que está sendo criado para atualizar na instância
            ShopItem shopItemPrefab = itemPrefabInstace.GetComponent<ShopItem>();
            shopItemPrefab.itemInfo = itemsToShow[i];
            //shopItemPrefab.GetComponent<ShopItem>().itemInfo.itemSprite = itemsToShow[i].itemSprite;

            // Verifica se o item já foi vendido
            // Se o item já foi vendido, chama a função ItemSold
            // Se o item não foi vendido, verifica se o player tem dinheiro pra comprar
            // Se não tiver dinheiro, muda o valor para vermelho e desabilita o botão
            if (toggleSelected.Contains("Avatar") && SaveManager.player.avatares[i])
            {
                shopItemPrefab.ItemSold();
            }
            else if (toggleSelected.Contains("Cores") && SaveManager.player.cores[i])
            {
                shopItemPrefab.ItemSold();
            }
            else if (toggleSelected.Contains("Extra") && SaveManager.player.extras[i])
            {
                shopItemPrefab.ItemSold();
            }
            else if (SaveManager.player.dinheiro < itemsToShow[i].itemPrice)
            {
                shopItemPrefab.transform.GetChild(3).GetComponent<Text>().color = Color.red;
                shopItemPrefab.transform.GetChild(5).GetComponent<Button>().interactable = false;
            }
        }
    }

    /// <summary>
    /// Destroi todos os itens que estão sendo exibidos na janela da loja
    /// </summary>
    private void DestroyShopItens()
    {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("ShopItem");

        for (int i = 0; i < objectsToDestroy.Length; i++)
        {
            Destroy(objectsToDestroy[i]);
        }
    }

    /// <summary>
    /// Confirma a operação de compra do item selecionado
    /// </summary>
    public void ConfirmTrade()
    {
        // Verifica em qual aba está e pega o valor do item e marca que ele já foi adquirido pelo player
        if (toggleSelected.Contains("Avatar"))
        {
            chosenItemPrice = avatarsToSell[chosenItem].itemPrice;
            SaveManager.player.avatares[chosenItem] = true;
            avatarsToSell[chosenItem].availableToBuy = false;
        }
        else if (toggleSelected.Contains("Cores"))
        {
            chosenItemPrice = colorsToSell[chosenItem].itemPrice;
            SaveManager.player.cores[chosenItem] = true;
            colorsToSell[chosenItem].availableToBuy = false;
        }
        else if (toggleSelected.Contains("Extras"))
        {
            chosenItemPrice = extrasToSell[chosenItem].itemPrice;
            SaveManager.player.extras[chosenItem] = true;
            extrasToSell[chosenItem].availableToBuy = false;
        }

        // Reduz o dinheiro que o player possui pelo valor do item comprado
        SaveManager.player.dinheiro = SaveManager.player.dinheiro - chosenItemPrice;
        // Atualiza na janela da loja o dinheiro atual do player e com a indicação que o item foi comprado
        playerMoney.GetComponent<Text>().text = SaveManager.player.dinheiro.ToString();
        Invoke(string.Concat(toggleSelected, "ShopWindow"), 0);
        // Destroi a janela de confirmação de compra
        Destroy(GameObject.Find("ConfirmMenuLoja(Clone)"));
        SaveManager.Save();
    }

    /// <summary>
    /// Instancia a janela de confirmação de compra
    /// </summary>
    public void CallConfirmMenu()
    {
        //GameObject confirmMenuInstance = 
        Instantiate(confirmMenu, GameObject.Find("Canvas").transform);
    }
}
