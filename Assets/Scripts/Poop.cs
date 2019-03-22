using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Poop : MonoBehaviour
{
    public void OnMouseDown()
    {
        Debug.Log("Entrou no OnMouseDown do Poop");
        ///Mudar valores da higiene
        ///Provavelmente invocará uma animação
        Destroy(transform.parent.gameObject);
        SaveManager.instance.player.poopLocation.Remove(SceneManager.GetActiveScene().name, gameObject.transform.position);
    }
}
