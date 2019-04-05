using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Petting : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 actualMousePosition;
    private Vector3 pastMousePosition;
    private bool isDragged = false;
    private int distanceLimit = 2;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Entrei, safado!");
        GameManager gameManager = GameManager.instance;
        gameManager.BlockSwipe = true;
        actualMousePosition = Input.mousePosition;
        isDragged = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        pastMousePosition = actualMousePosition;
        actualMousePosition = Input.mousePosition;
        if ((actualMousePosition.x - pastMousePosition.x > distanceLimit) || (actualMousePosition.y - pastMousePosition.y > distanceLimit)) 
        {
            Debug.Log("To dando carinho, desgraça!");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Larguei!");
    }
}
