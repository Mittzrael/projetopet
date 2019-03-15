using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Troca sprite de um spriterenderer. Objeto deve ter BoxCollider e isTrigger = true.
/// </summary>

public class ChangeSprite : MonoBehaviour
{
    private SpriteRenderer objetoParaMudarSprite;
    public Sprite sprite1;
    public Sprite sprite2;
    public bool isFixed = false;

    void Start()
    {
        objetoParaMudarSprite = this.GetComponent<SpriteRenderer>();
    }

    private void OnMouseUpAsButton()
    {
        if (objetoParaMudarSprite.sprite == sprite1)
        {
            objetoParaMudarSprite.sprite = sprite2;
            isFixed = true;
        }
        else
        {
            objetoParaMudarSprite.sprite = sprite1;
            isFixed = false;
        }
    }
}