using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food
{
    [SerializeField]
    private string nome;
    [SerializeField]
    private Image image;
    [SerializeField]
    private float nutritionalValor;
    //bool or float healthy;

    public float GetNutrionalValor()
    {
        return nutritionalValor;
    }
}
