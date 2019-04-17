using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWarning : MonoBehaviour
{
    public static PopUpWarning instance;
    [Tooltip("Lista contendo os avisos que devem aparecer")]
    public WarningsList[] warningsList;

    public List<string> warnings;
    public List<string> savedWarnings;

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

        for (int i = 0; i < transform.childCount; i++)
        {
            warnings.Add(transform.GetChild(i).name);
        }
        
        CallSavedWarnings();
    }

    public void CallSavedWarnings()
    {
        Debug.Log("callSW");
        foreach (string name in SaveManager.instance.player.savedWarnings)
        {
            CallWarning(name);
        }
    }

    public void CallAllWarnings(int listIndex)
    {
        Debug.LogWarning("callAW");
        for (int i = 0; i < warningsList[listIndex].warnings.Length; i++)
        {
            string name = warningsList[listIndex].warnings[i].warningName;
            if (!SaveManager.instance.player.savedWarnings.Contains(name))
            {
                SaveManager.instance.player.savedWarnings.Add(name);
                CallWarning(name);
            }
        }
    }

    public void SolveAllWarnings(int listIndex)
    {
        for (int i = 0; i < warningsList[listIndex].warnings.Length; i++)
        {
            SolveWarning(warningsList[listIndex].warnings[i].warningName);
            SaveManager.instance.player.savedWarnings.Clear();
        }
    }

    public void CallWarning(string warningName)
    {
        transform.GetChild(warnings.IndexOf(warningName)).gameObject.SetActive(true);
    }

    public void SolveWarning(string warningName)
    {
        transform.GetChild(warnings.IndexOf(warningName)).gameObject.SetActive(false);
        SaveManager.instance.player.savedWarnings.Remove(warningName);
    }

    private void Start()
    {
        //StartCoroutine(Teste());
    }

    private IEnumerator Teste()
    {
        yield return new WaitForSeconds(5);
        SolveAllWarnings(0);
        yield return new WaitForSeconds(5);
        CallAllWarnings(0);

        yield return new WaitForSeconds(5);

        SolveWarning("Hungry");
        SolveWarning("Poop");

        yield return new WaitForSeconds(5);

        CallWarning("Hungry");
        SolveWarning("Pee");
    }
}