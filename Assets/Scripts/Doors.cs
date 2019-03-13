using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public string sceneToLoad;

    private void OnMouseUpAsButton()
    {
        GameManager.instance.LoadSceneWithFade(sceneToLoad);
        GameObject.Find("dog_mitza").transform.position = new Vector3(0,-630,0);//ajusta posição na próxima tela, pode ser removido se os backgrounds forem padronizadas
    }
}
