using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para controlar os movimentos do animal. Estas funções podem ser chamadas de outras classes.
/// Este script deve estar no prefab do animal, que deve ter o animator correspondente.
/// </summary>
public class PetMovement : MonoBehaviour
{
    private Animator petAnimator;
    private SpriteRenderer petSpriteRenderer;

    public float speedWalk;
    public float speedRun;

    private Vector3 destination;
    public bool isWalking = false; //precisa ser public, é acessada por outro script (BasicPetAI)
    public bool isRunning = false; //precisa ser public, é acessada por outro script (BasicPetAI)

    void Awake()
    {
        petAnimator = GetComponent<Animator>();
        petSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isWalking) { 
            if (transform.position.x != destination.x)
            {
                petAnimator.SetBool("isWalking", true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(destination.x, transform.position.y, transform.position.z), speedWalk * Time.deltaTime);
            }
            else
            {
                petAnimator.SetBool("isWalking", false);
                isWalking = false;
            }
        }else if (isRunning){
            if (transform.position.x != destination.x)
            {
                petAnimator.SetBool("isRunning", true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(destination.x, transform.position.y, transform.position.z), speedRun * Time.deltaTime);
            }
            else
            {
                petAnimator.SetBool("isRunning", false);
                isWalking = false;
            }
        }
    }

    private void FlipXIfNeeded(float xPositionDestination)
    {
        destination.x = xPositionDestination;
        if (destination.x > transform.position.x)
        {
            petSpriteRenderer.flipX = true;
        }
        else
        {
            petSpriteRenderer.flipX = false;
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

    /// <summary>
    /// Aciona animação do animal coçando por 3 segundos (has exit time) (trigger) (para o gato ele lambe a pata).
    /// </summary>
    public void Cocando()
    {
        petAnimator.SetTrigger("isCocando");
    }

    /// <summary>
    /// Toggle animação do animal triste (true/false).
    /// </summary>
    public void ToggleSad()
    {
        petAnimator.SetBool("isSad", !petAnimator.GetBool("isSad"));
    }

    /// <summary>
    /// Aciona animação de espriguiçar;
    /// </summary>
    public void Espriguicar()
    {
        petAnimator.SetTrigger("isEspriguicando");
    }

    /// <summary>
    /// Toggle animação do animal deitado (true/false).
    /// </summary>
    public void ToggleDeitado()
    {
        petAnimator.SetBool("isDeitado", !petAnimator.GetBool("isDeitado"));
    }

    /// <summary>
    /// Aciona animação de uivar (somente cachorro);
    /// </summary>
    public void Howling()
    {
        petAnimator.SetTrigger("isHowling");
    }

    /// <summary>
    /// Toggle animação do animal bravo (true/false).
    /// </summary>
    public void ToggleAngry()
    {
        petAnimator.SetBool("isAngry", !petAnimator.GetBool("isAngry"));
    }
}
