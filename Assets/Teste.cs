using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teste : MonoBehaviour
{
    public GameObject pensante;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(teste());
    }

    public IEnumerator teste()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("kitchen", LoadSceneMode.Additive);
    }
}
