using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWarning : MonoBehaviour
{
    public WarningsList warningsList;
    public GameObject warningPrefab;

    public List<string> warnings;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            warnings.Add(transform.GetChild(i).name);
        }
    }

    public void CallWarning(string warningName)
    {
        transform.GetChild(warnings.IndexOf(warningName)).gameObject.SetActive(true);
    }

    public void SolveWarning(string warningName)
    {
        transform.GetChild(warnings.IndexOf(warningName)).gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(Teste());
    }

    private IEnumerator Teste()
    {
        yield return new WaitForSeconds(5);

        SolveWarning("Hungry");
        SolveWarning("Poop");

        yield return new WaitForSeconds(5);

        CallWarning("Hungry");
        SolveWarning("Pee");
    }
}