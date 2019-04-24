using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Esta classe só pode ser anexada a UI Buttons. 
/// Quando o mouse passa por cima do objeto anexado, o cursor alterna para a imagem aqui inserida.
/// O botão ao qual este script estiver anexado vai tocar um CLICK quando for apertado.
/// Também pode ser herdada.
/// </summary>

public class CursorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Texture2D cursor;
    private Button botao;
    AudioClip clickSound;

    private void Awake()
    {
        cursor = Resources.Load<Texture2D>("Images/cursor-edit-th");
        clickSound = (AudioClip)Resources.Load("Sounds/sfx/click_tecla01");
        botao = this.GetComponent<UnityEngine.UI.Button>();
    }

    public void Start()
    {
        botao.onClick.AddListener(delegate() { SoundManager.instance.PlaySFX(clickSound); });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (botao.interactable) {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
