using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    TimeManager instance = null;
    [SerializeField]
    private int currentPeriod;
    [SerializeField]
    private double[] timeBetweenPer;
    [SerializeField]
    private int numberOfPeriods;
    [SerializeField]
    private double limitTime;


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
    /*
    public void ForwardToNextPeriod()
    {

    }

    public void ResetPeriod()
    {

    }

    public IEnumerator ResetTimeCounter()
    {

    }

    public IEnumerator ResetTimeCounter(double time)
    {

    }

    public IEnumerator PeriodTimeCounter()
    {

    }

    public IEnumerator PeriodTimeCounter(double time)
    {

    }

    public void TimeBetweenSessions()
    {

    }
    */
}
