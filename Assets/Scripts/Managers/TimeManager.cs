using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimeHelper
{
    public int currentPeriod;
    public int dayCounter;
    public string lastMeal = System.DateTime.UtcNow.ToString();
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
    private double limitTime;
    private SaveManager saveManager;
    
    #region Getters & Setters
    /// <summary>
    /// Retorna o período atual
    /// </summary>
    /// <returns></returns>
    public int GetCurrentPeriod()
    {
        return currentPeriod;
    }

    /// <summary>
    /// Retorna o tempo de espera após o período i
    /// </summary>
    /// <param name="i">Período que se quer saber o tempo de espera após ele</param>
    /// <returns>Tempo de espera</returns>
    public double GetTimeBetweenPeriods(int i)
    {
        return timeBetweenPeriods[i];
    }

    /// <summary>
    /// Retorna o tempo, em segundos, máximo em que é possível ficar sem alimentar o pet.
    /// </summary>
    /// <returns></returns>
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
        saveManager = SaveManager.instance;
        currentPeriod = SaveManager.instance.player.timeHelper.currentPeriod;
    }

    /// <summary>
    /// Função que é chamada para dizer como que o período procederá, podendo resetá-lo, avançá-lo ou então só fazer o contador continuar.
    /// </summary>
    public void PeriodProcess()
    {
        if (TimeSinceMeal() > limitTime)
        {
            Debug.Log("Entrou no 1");
            ResetPeriod();
        }

        else if (TimeSinceMeal() > timeBetweenPeriods[currentPeriod] && SaveManager.instance.player.timeHelper.betweenMealAndPeriod)
        {
            Debug.Log("Entrou no 2");
            ForwardToNextPeriod();
        }

        else
        {
            Debug.Log("Entrou no 3");
            StartTimerCount();
        }
    }

    /// <summary>
    /// Inicia a contagens para caso haja necessidade do periodo avançar enquanto o jogador está jogando.
    /// </summary>
    private void StartTimerCount()
    {
        StopAllCoroutines();

        StartCoroutine(StartPeriodTimeCount());
        StartCoroutine(StartResetTimeCount());
    }

    /// <summary>
    /// Passa para o próximo período (ciclo normal)
    /// </summary>
    private void ForwardToNextPeriod()
    {
        Debug.Log("Entrou no Forward");
        currentPeriod++;
        currentPeriod = (currentPeriod % numberOfPeriods);
        if (currentPeriod == 0)
        {
          DayCounterUp();
        }
        saveManager.player.timeHelper.betweenMealAndPeriod = false;
        saveManager.player.timeHelper.currentPeriod = currentPeriod;
        GameManager.instance.StartPeriod();
    }

    /// <summary>
    /// Passa para o período 0 direto, caso o jogador passe mais de 48h sem alimentar.
    /// </summary>
    private void ResetPeriod()
    {
        SaveManager saveManager = SaveManager.instance;
        saveManager.player.timeHelper.currentPeriod = 0;
        currentPeriod = 0;
        saveManager.player.timeHelper.lastMeal = "";
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

    public void StopResetTime()
    {
        StopCoroutine(StartResetTimeCount());
    }

    /// <summary>
    /// Verifica de tempos em tempos se o tempo do período já foi excedido.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartPeriodTimeCount()
    {
        Debug.Log(SaveManager.instance.player.timeHelper.betweenMealAndPeriod);
        if (SaveManager.instance.player.timeHelper.betweenMealAndPeriod)
        {
            if (TimeSinceMeal() > timeBetweenPeriods[currentPeriod])
            {
                SaveManager.instance.player.timeHelper.betweenMealAndPeriod = false;
                ForwardToNextPeriod();
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
        saveManager.player.timeHelper.dayCounter++;
    }
}