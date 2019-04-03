using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PotStatus
{
    public Food content; //Conteúdo do pote
    [SerializeField]
    private float potFullness; //Quão cheio está o pote

    public PotStatus() { }
    public PotStatus(Food food)
    {
        content = food;
    }

    public void SetFullness(float valor)
    {
        potFullness += valor;
        potFullness = Mathf.Clamp(valor, 0, 1);
    }

    public float GetFullness()
    {
        return potFullness;
    }
}
