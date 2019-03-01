using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Health
{
    [SerializeField]
    private float hungry; //Quanta fome o animal está sentido, quanto mais alto melhor para a saúde
    [SerializeField]
    private float thirst; //Quanta sede o animal está sentido, quanto mais alto melhor para a saúde
    [SerializeField]
    private float hygiene; //Qual é a higiene do animal, quanto mais alto melhor para a saúde

    public Health()
    {
        hungry = 1;
        thirst = 1;
        hygiene = 1;
    }

    /// <summary>
    /// Recebe o valor da fome do animal.
    /// </summary>
    /// <returns>Recebe um valor entre 0 e 1 sendo 0, esfomeado, e 1, satisfeito</returns>
    public float GetHungry()
    {
        return hungry;
    }

    /// <summary>
    /// Adiciona um valor a fome do animal.
    /// </summary>
    /// <param name="i">Valor de fome que é adicionado, se for positivo, animal comeu algo, se for negativo, animal está ficando faminto</param>
    public void PutInHungry(float i)
    {
        hungry += hungry;
        hungry = Mathf.Clamp(hungry, 0, 1);
    }

    /// <summary>
    /// Recebe o valor da sede do animal.
    /// </summary>
    /// <returns>Recebe um valor entre 0 e 1 sendo 0, com desidratado, e 1, hidratado</returns>
    public float GetThirst()
    {
        return thirst;
    }

    /// <summary>
    /// Adiciona um valor a sede do animal.
    /// </summary>
    /// <param name="i">Valor de sede que é adicionado, se for positivo, animal bebeu algo, se for negativo, animal está ficando com sede</param>
    public void PutInThirst(float i)
    {
        thirst += thirst;
        thirst = Mathf.Clamp(thirst, 0, 1);
    }

    /// <summary>
    /// Recebe o valor da higiene do animal.
    /// </summary>
    /// <returns>Recebe um valor entre 0 e 1 sendo 0, sujo, e 1, limpo</returns>
    public float GetHygiene()
    {
        return hygiene;
    }

    /// <summary>
    /// Adiciona um valor a higiene do animal.
    /// </summary>
    /// <param name="i">Valor de sede que é adicionado, se for positivo, animal está ficando limpo, se for negativo, animal está ficando sujo</param>
    public void PutInHygiene(float i)
    {
        hygiene += hygiene;
        hygiene = Mathf.Clamp(hygiene, 0, 1);
    }
}
