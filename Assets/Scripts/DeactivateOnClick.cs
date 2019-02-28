using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnClick : MonoBehaviour
{
    public bool isCleaned;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseUpAsButton()
    {
        animator.Play("ObjectVanish");
        isCleaned = true;
    }
}
