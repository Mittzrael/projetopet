using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogNewz : MonoBehaviour
{
    private Animator dogNewzAnimator;
    private SpriteRenderer dogNewzSpriteRenderer;

    public float speedWalk;
    public float speedRun;
   // Vector3 targetPos;
   // public Transform target;

    Vector3 destination;
    public float xAxisDestination;
    bool isWalking = false;
    bool isRunning = false;

    void Start()
    {
        destination = new Vector3(xAxisDestination, transform.position.y, transform.position.z);

       // targetPos = transform.position;
        dogNewzAnimator = GetComponent<Animator>();
        dogNewzSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    targetPos = (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    target.position = targetPos;
        //    if (targetPos.x < transform.position.x)
        //    {
        //        dogNewzSpriteRenderer.flipX = true;
        //    }
        //    else
        //    {
        //        dogNewzSpriteRenderer.flipX = false;
        //    }
        //}
        //if (transform.position.x != targetPos.x)
        //{
        //    dogNewzAnimator.SetBool("isWalking",true);
        //    transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPos.x,transform.position.y,transform.position.z), speed * Time.deltaTime);
        //}
        //else
        //{
        //    dogNewzAnimator.SetBool("isWalking",false);
        //}
        if (isWalking) { 
            if (transform.position.x != destination.x)
            {
                dogNewzAnimator.SetBool("isWalking", true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(destination.x, transform.position.y, transform.position.z), speedWalk * Time.deltaTime);
            }
            else
            {
                dogNewzAnimator.SetBool("isWalking", false);
                isWalking = false;
            }
        }
        if (isRunning){
            if (transform.position.x != destination.x)
            {
                dogNewzAnimator.SetBool("isRunning", true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(destination.x, transform.position.y, transform.position.z), speedRun * Time.deltaTime);
            }
            else
            {
                dogNewzAnimator.SetBool("isRunning", false);
                isWalking = false;
            }
        }
    }

    //private void OnMouseUpAsButton()
    //{
    //    dogNewzAnimator.SetTrigger("dog_hurt");
    //}

    public void MoveAnimalAndando()
    {
        isWalking = true;
        if (destination.x < transform.position.x)
        {
            dogNewzSpriteRenderer.flipX = true;
        }
        else
        {
            dogNewzSpriteRenderer.flipX = false;
        }
    }

    public void MoveAnimalCorrendo()
    {
        isRunning = true;
        if (destination.x < transform.position.x)
        {
            dogNewzSpriteRenderer.flipX = true;
        }
        else
        {
            dogNewzSpriteRenderer.flipX = false;
        }
    }

    public void AnimalHurt()
    {
        dogNewzAnimator.SetTrigger("dog_hurt");
    }
}
