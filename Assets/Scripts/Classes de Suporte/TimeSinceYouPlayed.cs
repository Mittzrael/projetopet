using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSinceYouPlayed
{
    /// <summary>
    /// Retorna a quantidade de tempo que passou desde a última vez que o jogo foi ligado (em segundos)
    /// </summary>
    /// <returns></returns>
    public static double TimeElapsedSeconds()
    {
        //SaveManager.instance.Load(0);
        System.DateTime nowTime = System.DateTime.UtcNow; //Data atual
        System.TimeSpan timeElapsed = nowTime - System.Convert.ToDateTime(SaveManager.instance.player.lastTimePlayed); //Tempo atual - tempo da última vez que foi jogado
        return timeElapsed.TotalSeconds;
    }

    /// <summary>
    /// Retorna a quantidade de tempo que passou desde a última vez que o jogo foi ligado (em minutos)
    /// </summary>
    /// <returns></returns>
    public static double TimeElapsedMinutes()
    {
        //SaveManager.instance.Load(0);
        System.DateTime nowTime = System.DateTime.UtcNow; //Data atual
        System.TimeSpan timeElapsed = nowTime - System.Convert.ToDateTime(SaveManager.instance.player.lastTimePlayed); //Tempo atual - tempo da última vez que foi jogado
        return timeElapsed.TotalMinutes;
    }

    /// <summary>
    /// Retorna a quantidade de tempo que passou desde a última vez que o jogo foi ligado (em horas)
    /// </summary>
    /// <returns></returns>
    public static double TimeElapsedHours()
    {
        //SaveManager.instance.Load(0);
        System.DateTime nowTime = System.DateTime.UtcNow; //Data atual
        System.TimeSpan timeElapsed = nowTime - System.Convert.ToDateTime(SaveManager.instance.player.lastTimePlayed); //Tempo atual - tempo da última vez que foi jogado
        return timeElapsed.TotalHours;
    }

    /// <summary>
    /// Retorna a quantidade de tempo que passou desde a última vez que o jogo foi ligado (em dias)
    /// </summary>
    /// <returns></returns>
    public static double TimeElapsedDays()
    {
        //SaveManager.instance.Load(0);
        System.DateTime nowTime = System.DateTime.UtcNow; //Data atual
        System.TimeSpan timeElapsed = nowTime - System.Convert.ToDateTime(SaveManager.instance.player.lastTimePlayed); //Tempo atual - tempo da última vez que foi jogado
        return timeElapsed.TotalDays;
    }
}
