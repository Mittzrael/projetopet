using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class ShopToggleWindow : MonoBehaviour, IPointerClickHandler {

    #region Variáveir Globais
    private ToggleGroup toggleGroup;
    private ShopManager shopManager;
    #endregion

    void Awake () {
        // Encontra a referência do painel de seleção
        toggleGroup = FindObjectOfType<ToggleGroup>();
    }
	
    void Start()
    {
        // Encontra a referência do ShopManager
        shopManager = FindObjectOfType<ShopManager>();
    }

    /// <summary>
    /// Verifica qual item do painel de seleção foi escolhido no click do mouse
    /// </summary>
    /// <param name="pointerClick"></param>
    public void OnPointerClick(PointerEventData pointerClick)
    {
        // Pega o nome do primeir item selecionado no painel
        // Como o painel só pode ter um selecionado ao mesmo tempo, pega o único escolhido
        string tempToggleName = toggleGroup.ActiveToggles().FirstOrDefault().name;
        
        // Junta o nome do selecionado com Selected pra chamar a função responsável por aquela aba
        tempToggleName = string.Concat(tempToggleName, "Selected");
        Invoke(tempToggleName, 0);
    }

    /// <summary>
    /// Mostra a aba dos avatares
    /// </summary>
    private void AvatarToggleSelected()
    {
        shopManager.AvatarToggleShopWindow();
    }

    /// <summary>
    /// Mostra a aba das cores
    /// </summary>
    private void CoresToggleSelected()
    {
        shopManager.CoresToggleShopWindow();
    }

    /// <summary>
    /// Mostra a aba dos extras
    /// </summary>
    private void ExtrasToggleSelected()
    {
        shopManager.ExtrasToggleShopWindow();
    }

    /// <summary>
    /// Retorna o nome da aba selecionada
    /// </summary>
    /// <returns></returns>
    public string SelectedToggle()
    {
        return toggleGroup.ActiveToggles().FirstOrDefault().name;
    }
}
