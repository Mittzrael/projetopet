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
    private float thirsty; //Quanta sede o animal está sentido, quanto mais alto melhor para a saúde
    [SerializeField]
    private float hygiene; //Qual é a higiene do animal, quanto mais alto melhor para a saúde

    [SerializeField]
    private float happiness; // Indica a felicidade do animal (se quer brincar ou não)
    [SerializeField]
    private float whatToPlay; // Indica se o pet sabe com o que pode brincar
    [SerializeField]
    private float pee; // Indica se o pet quer fazer xixi
    [SerializeField]
    private float poop; // Indica se o pet quer fazer cocô
    [SerializeField]
    private float whereToPP; // Indica se o pet sabe onde fazer xixi/cocô

    public Health()
    {
        hungry = 1;
        thirsty = 1;
        hygiene = 1;
        happiness = 1;
        whatToPlay = 0;
        pee = 0;
        poop = 0;
        whereToPP = 0;
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
        hungry += i;
        hungry = Mathf.Clamp(hungry, 0, 1);
    }

    /// <summary>
    /// Recebe o valor da sede do animal.
    /// </summary>
    /// <returns>Recebe um valor entre 0 e 1 sendo 0, com desidratado, e 1, hidratado</returns>
    public float GetThirsty()
    {
        return thirsty;
    }

    /// <summary>
    /// Adiciona um valor a sede do animal.
    /// </summary>
    /// <param name="i">Valor de sede que é adicionado, se for positivo, animal bebeu algo, se for negativo, animal está ficando com sede</param>
    public void PutInThirsty(float i)
    {
        thirsty += i;
        thirsty = Mathf.Clamp(thirsty, 0, 1);
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
        hygiene += i;
        hygiene = Mathf.Clamp(hygiene, 0, 1);
    }

    /// <summary>
    /// Recebe o valor da felicidade do animal
    /// </summary>
    /// <returns>Recebe um valor entre 0 e 1, onde 0 é triste e 1 é feliz</returns>
    public float GetHappiness()
    {
        return happiness;
    }

    /// <summary>
    /// Adiciona um valor a felicidade do animal
    /// </summary>
    /// <param name="i">Valor de felicidade que é adicionado, se for positivo, animal está ficando feliz, se for negativo, animal está ficando triste</param>
    public void PutInHappiness(float i)
    {
        happiness += i;
        happiness = Mathf.Clamp(happiness, 0, 1);
    }
    
    /// <summary>
    /// Recebe o valor do treinamento do animal em relação ao que ele pode brincar
    /// </summary>
    /// <returns>Recebe um valor entre 0 e 1, onde 0 é não treinado e 1 treinado</returns>
    public float GetWhatToPlay()
    {
        return whatToPlay;
    }

    /// <summary>
    /// Adiciona um valor ao treinamento do animal em relação a brincadeiras/comportamento
    /// </summary>
    /// <param name="i">Valor de treinamento que é adicionado, se for positivo, animal sabe com o que pode brincar, se for negativo, animal não sabe com o que pode brincar</param>
    public void PutInWhatToPlay(float i)
    {
        whatToPlay += i;
        whatToPlay = Mathf.Clamp(whatToPlay, 0, 1);
    }

    /// <summary>
    /// Recebe o valor da vontade de fazer xixi do animal
    /// </summary>
    /// <returns>Recebe um valor entre 0 e 1, onde 0 é nenhuma vontade e 1 com muita vontade</returns>
    public float GetPee()
    {
        return pee;
    }

    /// <summary>
    /// Adiciona um valor a vontade de fazer xixi do animal
    /// </summary>
    /// <param name="i">Valor da vontade de fazer xixi que é adicionado, se for positivo, animal está ficando com vontade de fazer xixi</param>
    public void PutInPee(float i)
    {
        pee += i;
        pee = Mathf.Clamp(pee, 0, 1);
    }

    /// <summary>
    /// Recebe o valor da vontade de fazer cocô do animal
    /// </summary>
    /// <returns>Recebe um valor entre 0 e 1, onde 0 é nenhuma vontade e 1 com muita vontade</returns>
    public float GetPoop()
    {
        return poop;
    }

    /// <summary>
    /// Adiciona um valor a vontade de fazer cocô do animal
    /// </summary>
    /// <param name="i">Valor da vontade de fazer cocô que é adicionado, se for positivo, animal está ficando com vontade</param>
    public void PutInPoop(float i)
    {
        poop += i;
        poop = Mathf.Clamp(poop, 0, 1);
    }

    /// <summary>
    /// Recebe o valor do treinamento do animal em relação a local de necessidades
    /// </summary>
    /// <returns>Recebe um valor entre 0 e 1, onde 0 é não treinado e 1 treinado</returns>
    public float GetWhereToPP()
    {
        return whereToPP;
    }

    /// <summary>
    /// Adiciona um valor ao treinamento do animal em relação a xixi/cocô
    /// </summary>
    /// <param name="i">Valor do treinamento xixi/cocô que é adicionado, se for positivo, animal está aprendendo onde fazer xixi/cocô</param>
    public void PutInWhereToPP(float i)
    {
        whereToPP += i;
        whereToPP = Mathf.Clamp(whereToPP, 0, 1);
    }

}