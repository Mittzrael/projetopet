using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teste : MonoBehaviour
{
    GameObject go;

    public void Butt()
    {
        go = GameObject.Find("PetFather");
        StartCoroutine(go.GetComponent<Invisible>().PetChangeLocation("Blabla"));
    }
}
