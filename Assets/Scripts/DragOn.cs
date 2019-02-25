using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Função para jogar em um objeto para poder arrastá-lo
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DragOn : MonoBehaviour
{
    private float distance = 10;

    public void OnMouseDown()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.BlockSwipe = true;
        OnMouseDrag();
    }

    public void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    public void OnMouseUp()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.BlockSwipe = false;
    }
}
