using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Health
{
    [SerializeField]
    private float hungry;
    [SerializeField]
    private float thirst;
    [SerializeField]
    private float hygiene;

    public Health()
    {
        hungry = 1;
        thirst = 1;
        hygiene = 1;
    }

    public float GetHungry()
    {
        return hungry;
    }

    public void PutInHungry(float i)
    {
        i = (i / 100);
        hungry += hungry;
    }

    public float GetThirst()
    {
        return thirst;
    }

    public void PutInThirst(float i)
    {
        i = (i / 100);
        thirst += thirst;
    }

    public float GetHygiene()
    {
        return hygiene;
    }

    public void PutInHygiene(float i)
    {
        i = (i / 100);
        hygiene += hygiene;
    }
}
