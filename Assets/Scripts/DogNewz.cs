using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogNewz : MonoBehaviour
{
    private Animator dogNewzAnimator;
    private SpriteRenderer dogNewzSpriteRenderer;

    public float speed;
    Vector3 targetPos;
    public Transform target;

    void Start()
    {
        targetPos = transform.position;
        dogNewzAnimator = GetComponent<Animator>();
        dogNewzSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetPos = (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.position = targetPos;
            if (targetPos.x < transform.position.x)
            {
                dogNewzSpriteRenderer.flipX = true;
            }
            else
            {
                dogNewzSpriteRenderer.flipX = false;
            }
        }
        if (transform.position.x != targetPos.x)
        {
            dogNewzAnimator.SetBool("isWalking",true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPos.x,transform.position.y,transform.position.z), speed * Time.deltaTime);
        }
        else
        {
            dogNewzAnimator.SetBool("isWalking",false);
        }
    }

    private void OnMouseUpAsButton()
    {
        dogNewzAnimator.SetTrigger("dog_hurt");
    }
}
