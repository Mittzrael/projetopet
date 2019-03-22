using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pee : MonoBehaviour
{
    public void OnMouseDown()
    {
        Debug.Log("Entrou no MouseDown do pee");
        ///Mudar valores da higiene
        ///Provavelmente invocará uma animação
        Destroy(transform.parent.gameObject);
        SaveManager.instance.player.peeLocation.Remove(SceneManager.GetActiveScene().name, gameObject.transform.position);
    }
}