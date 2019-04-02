using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Food
{
    [SerializeField]
    private string nome;
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private float nutritionalValor;
    //bool or float healthy;

    public Food() { }
    public Food(string name, float value)
    {
        nome = name;
        nutritionalValor = value;
    }

    public float GetNutrionalValor()
    {
        return nutritionalValor;
    }
}
