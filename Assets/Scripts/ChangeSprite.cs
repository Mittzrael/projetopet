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

    //quando mudar para sprite2 atribui true
    public bool isFixed = false;

    // Start is called before the first frame update
    void Start()
    {
        objetoParaMudarSprite = this.GetComponent<SpriteRenderer>();

        //sprite2 = Resources.Load<Sprite>("Images/Iori_Yagami");
    }

    // Update is called once per frame
    void Update()
    {
        //if ()
        //{

        //}
    }

    private void OnMouseUpAsButton()
    {
        if (objetoParaMudarSprite.sprite == sprite1)
        {
            //dog.sprite = iori;
            objetoParaMudarSprite.sprite = sprite2;
            isFixed = true;
        }
        else
        {
            objetoParaMudarSprite.sprite = sprite1;
            isFixed = false;
        }
    }


    /// <summary>
    /// Método para substituir o sprite ao clicar
    /// </summary>
    /// <param name="num"></param>
    //public void MudarSprite(int num)
    //{

    //}


}