using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance = null;
    [SerializeField]
    private int currentPeriod;
    [SerializeField]
    private double[] timeBetweenPeriods;
    [SerializeField]
    private int numberOfPeriods;
    [SerializeField]
    private double limitTime;
    [SerializeField]
    private double timeSinceLastMeal;

    public bool limitTimeRunning;

    #region Getters & Setters
    public int GetCurrentPeriod()
    {
        return currentPeriod;
    }

    public double GetTimeBetweenPeriods(int i)
    {
        return timeBetweenPeriods[i];
    }

    public double GetLimitTime()
    {
        return limitTime;
    }
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        currentPeriod = SaveManager.instance.player.currentPeriod;
    }

    /// <summary>
    /// Passa para o próximo período (ciclo normal)
    /// </summary>
    public void ForwardToNextPeriod()
    {
        Debug.Log("Entrou no Forward");
        currentPeriod++;
        currentPeriod = (currentPeriod % numberOfPeriods);
        if (currentPeriod == 0)
        {
          DayCounterUp();
        }
        GameManager.instance.StartPeriod();
    }

    public void PeriodChecker()
    {
        if (TimeSinceMeal() > limitTime)
        {
            Debug.Log("Entrou no 1");
            ResetPeriod();
        }

        else if (TimeSinceMeal() > timeBetweenPeriods[currentPeriod])
        {
            Debug.Log("Entrou no 2");
            ForwardToNextPeriod();
        }

        else
        {
            Debug.Log("Entrou no 3");
            GameManager.instance.StartPeriod();
        }
    }

    /// <summary>
    /// Passa para o período 0 direto, caso o jogador passe mais de 48h sem alimentar.
    /// </summary>
    public void ResetPeriod()
    {
        SaveManager.instance.player.currentPeriod = 0;
        currentPeriod = 0;
        SaveManager.instance.player.lastMeal = "";
        limitTimeRunning = false;
        ///Momento de chamar alguma bronca aqui sendo que a próxima linha pode ir para o final da função que toca a animação.
        GameManager.instance.StartPeriod();
    }

    /// <summary>
    /// Tempo que conta até o ResetSerPermitido
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartResetTimeCount()
    {
        limitTimeRunning = true;
        if (TimeSinceMeal() > limitTime)
        {
            ResetPeriod();
        }
        else
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(StartResetTimeCount());
        }
    }

    /// <summary>
    /// Verifica de tempos em tempos se o tempo do período já foi excedido.
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartPeriodTimeCount()
    {
        if (TimeSinceMeal() > timeBetweenPeriods[currentPeriod])
        {
            Debug.Log("Chamou o forward");
            ForwardToNextPeriod();
        }

        else
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(StartPeriodTimeCount());
        }
    }

    /// <summary>
    /// Conta o tempo desde a última refeição.
    /// </summary>
    /// <returns>Tempo desde a última refeição</returns>
    public static double TimeSinceMeal()
    {
        System.DateTime nowTime = System.DateTime.UtcNow; //Data atual
        System.TimeSpan timeElapsed = nowTime - System.Convert.ToDateTime(SaveManager.instance.player.lastMeal); //Tempo atual - tempo da última vez que foi jogado
        Debug.Log("Foi chamado, passaram " + timeElapsed.TotalSeconds.ToString() + " segundos");
        return timeElapsed.TotalSeconds;
    }

    /// <summary>
    /// Aumenta o valor do contador de dias
    /// </summary>
    private void DayCounterUp()
    {
        SaveManager.instance.player.dayCounter++;
    }
}
