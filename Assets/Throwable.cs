using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Throwable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector2 force;
    public float multiplier = 5;
    private Vector3 pastPosition;
    private Vector3 actualPosition;
    private bool isDragged = false;
    private float distance = 0;
    private Rigidbody2D rb2D;
    private Rigidbody2D rb2DTemp;
    /*
    public void OnMouseDown()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.BlockSwipe = true;
        actualPosition = transform.position;
        isDragged = true;
        StartCoroutine(ForceApplied());
    }

    public void OnMouseUp()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.BlockSwipe = false;
        force = new Vector2((actualPosition.x - pastPosition.x), (actualPosition.y - pastPosition.y));
        Debug.Log(force.x);
        Debug.Log(force.y);
        gameObject.GetComponent<Rigidbody2D>().AddForce(force*multiplier, ForceMode2D.Impulse);
        isDragged = false;
    }*/

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameManager gameManager = GameManager.instance;
        TakeOutPhysics();
        gameManager.BlockSwipe = true;
        actualPosition = transform.position;
        isDragged = true;
        StartCoroutine(ForceApplied());
    }

    private void TakeOutPhysics()
    {
        rb2DTemp = new Rigidbody2D();
        rb2D = GetComponent<Rigidbody2D>();
        rb2DTemp.gravityScale = rb2D.gravityScale;
        rb2DTemp.drag = rb2D.drag;
        rb2DTemp.mass = rb2D.mass;
        rb2D.gravityScale = 0;
        rb2D.drag = 0;
        rb2D.mass = 0;
    }

    private void GiveAgainPhysics()
    {
        rb2D.drag = rb2DTemp.drag;
        rb2D.gravityScale = rb2DTemp.gravityScale;
        rb2D.mass = rb2DTemp.mass;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    public IEnumerator ForceApplied()
    {
        while (isDragged)
        {
            pastPosition = actualPosition;
            actualPosition = transform.position;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        GameManager gameManager = GameManager.instance;
        gameManager.BlockSwipe = false;
        force = new Vector2((actualPosition.x - pastPosition.x), (actualPosition.y - pastPosition.y));
        Debug.Log(force.x);
        Debug.Log(force.y);
        gameObject.GetComponent<Rigidbody2D>().AddForce(force * multiplier, ForceMode2D.Impulse);
        isDragged = false;
    }

}
