using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimeHelper
{
    public int currentPeriod;
    public int dayCounter;
    public string lastMeal;
    public bool betweenMealAndPeriod = false;
    public bool betweenMealAndTimeLimit = false;
}

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
    private double limitTime = 60;
    [SerializeField]
    private double timeSinceLastMeal;

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
        currentPeriod = SaveManager.instance.player.timeHelper.currentPeriod;
    }

    public void StartTimerCount()
    {
        StartCoroutine(StartPeriodTimeCount());
        StartCoroutine(StartResetTimeCount());
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
        SaveManager.instance.player.timeHelper.betweenMealAndPeriod = false;
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
        SaveManager saveManager = SaveManager.instance;
        SaveManager.instance.player.timeHelper.currentPeriod = 0;
        currentPeriod = 0;
        SaveManager.instance.player.timeHelper.lastMeal = "";
        saveManager.player.timeHelper.betweenMealAndTimeLimit = false;
        saveManager.player.timeHelper.betweenMealAndPeriod = false;
        ///Momento de chamar alguma bronca aqui sendo que a próxima linha pode ir para o final da função que toca a animação.
        GameManager.instance.StartPeriod();
    }

    /// <summary>
    /// Tempo que conta até o ResetSerPermitido
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartResetTimeCount()
    {
        //yield return new WaitForSeconds(1f);
        Debug.Log(SaveManager.instance.player.timeHelper.betweenMealAndTimeLimit);
        if (SaveManager.instance.player.timeHelper.betweenMealAndTimeLimit)
        {
            if (TimeSinceMeal() > limitTime)
            {
                SaveManager.instance.player.timeHelper.betweenMealAndTimeLimit = false;
                ResetPeriod();
            }
            else
            {
                yield return new WaitForSeconds(5f);
                StartCoroutine(StartResetTimeCount());
            }
        }
    }

    /// <summary>
    /// Verifica de tempos em tempos se o tempo do período já foi excedido.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartPeriodTimeCount()
    {
        //yield return new WaitForSeconds(1f);
        Debug.Log(SaveManager.instance.player.timeHelper.betweenMealAndPeriod);
        if (SaveManager.instance.player.timeHelper.betweenMealAndPeriod)
        {
            if (TimeSinceMeal() > timeBetweenPeriods[currentPeriod])
            {
                ForwardToNextPeriod();
                SaveManager.instance.player.timeHelper.betweenMealAndPeriod = false;
            }
            else
            {
                yield return new WaitForSeconds(5f);
                StartCoroutine(StartPeriodTimeCount());
            }
        }
    }

    /// <summary>
    /// Conta o tempo desde a última refeição.
    /// </summary>
    /// <returns>Tempo desde a última refeição</returns>
    public static double TimeSinceMeal()
    {
        System.DateTime nowTime = System.DateTime.UtcNow; //Data atual
        System.TimeSpan timeElapsed = nowTime - System.Convert.ToDateTime(SaveManager.instance.player.timeHelper.lastMeal); //Tempo atual - tempo da última vez que foi jogado
        Debug.Log("Foi chamado, passaram " + timeElapsed.TotalSeconds.ToString() + " segundos");
        return timeElapsed.TotalSeconds;
    }

    /// <summary>
    /// Aumenta o valor do contador de dias
    /// </summary>
    private void DayCounterUp()
    {
        SaveManager.instance.player.timeHelper.dayCounter++;
    }
}
