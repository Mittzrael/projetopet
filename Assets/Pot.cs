using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot: MonoBehaviour
{
    private GameObject potSprite; //Sprite do pote
    public PotStatus pot;

    private void Awake()
    {
        potSprite = transform.GetChild(0).gameObject;
    }
}
