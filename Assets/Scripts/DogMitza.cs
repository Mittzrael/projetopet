using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMitza : MonoBehaviour
{
    private Animator dogMitzaAnimator;
    private SpriteRenderer dogMitzaSpriteRenderer;

    public float speedWalk;
    public float speedRun;

    Vector3 destination;
    bool isWalking = false;
    bool isRunning = false;

    void Start()
    {
        dogMitzaAnimator = GetComponent<Animator>();
        dogMitzaSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isWalking) { 
            if (transform.position.x != destination.x)
            {
                dogMitzaAnimator.SetBool("isWalking", true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(destination.x, transform.position.y, transform.position.z), speedWalk * Time.deltaTime);
            }
            else
            {
                dogMitzaAnimator.SetBool("isWalking", false);
                isWalking = false;
            }
        }
        if (isRunning){
            if (transform.position.x != destination.x)
            {
                dogMitzaAnimator.SetBool("isRunning", true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(destination.x, transform.position.y, transform.position.z), speedRun * Time.deltaTime);
            }
            else
            {
                dogMitzaAnimator.SetBool("isRunning", false);
                isWalking = false;
            }
        }
    }

    public void MoveAnimalAndando(float xPositionDestination)
    {
        destination.x = xPositionDestination;
        isWalking = true;
        if (destination.x > transform.position.x)
        {
            dogMitzaSpriteRenderer.flipX = true;
        }
        else
        {
            dogMitzaSpriteRenderer.flipX = false;
        }
    }

    public void MoveAnimalCorrendo(float xPositionDestination)
    {
        destination.x = xPositionDestination;
        isRunning = true;
        if (destination.x < transform.position.x)
        {
            dogMitzaSpriteRenderer.flipX = true;
        }
        else
        {
            dogMitzaSpriteRenderer.flipX = false;
        }
    }

    public void Cocando()
    {
        dogMitzaAnimator.SetTrigger("isCocando");
    }

    public void ToggleSad()
    {
        dogMitzaAnimator.SetBool("isSad", !dogMitzaAnimator.GetBool("isSad"));
    }
}
