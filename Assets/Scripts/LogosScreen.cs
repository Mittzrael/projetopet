using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogosScreen : MonoBehaviour
{
    private GameObject logo2;

    void Start()
    {
        logo2 = GameObject.Find("LogoUFABC");
        logo2.SetActive(false);
        StartCoroutine(PlayLogo2());
    }

    IEnumerator PlayLogo2()
    {
        yield return new WaitForSeconds(4);
        logo2.SetActive(true);
        yield return new WaitForSeconds(5);
        //chamar proxima cena
    }    
}
