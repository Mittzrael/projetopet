using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMitza : MonoBehaviour
{
    private Animator dogMitzaAnimator;
    private SpriteRenderer dogMitzaSpriteRenderer;

    public float speedWalk;
    public float speedRun;

    private Vector3 destination;
    private bool isWalking = false;
    private bool isRunning = false;

    void Awake()
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

    /// <summary>
    /// Aciona animação do dog andando e movimenta objeto até o ponto de destino.
    /// </summary>
    /// <param name="xPositionDestination">Posição X de destino.</param>
    public void MoveAnimalAndando(float xPositionDestination)
    {
        FlipXIfNeeded(xPositionDestination);
        isWalking = true;      
    }

    /// <summary>
    /// Aciona animação do dog correndo e movimenta objeto até o ponto de destino.
    /// </summary>
    /// <param name="xPositionDestination">Posição X de destino.</param>
    public void MoveAnimalCorrendo(float xPositionDestination)
    {
        FlipXIfNeeded(xPositionDestination);
        isRunning = true;       
    }

    public void FlipXIfNeeded(float xPositionDestination)
    {
        destination.x = xPositionDestination;
        if (destination.x > transform.position.x)
        {
            dogMitzaSpriteRenderer.flipX = true;
        }
        else
        {
            dogMitzaSpriteRenderer.flipX = false;
        }
    }

    /// <summary>
    /// Aciona animação do dog coçando por 3 segundos (has exit time) (trigger).
    /// </summary>
    public void Cocando()
    {
        dogMitzaAnimator.SetTrigger("isCocando");
    }

    /// <summary>
    /// Toggle animação do dog triste (true/false).
    /// </summary>
    public void ToggleSad()
    {
        dogMitzaAnimator.SetBool("isSad", !dogMitzaAnimator.GetBool("isSad"));
    }
}
