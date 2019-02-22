using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [Tooltip("Música para tocar.")]
    public AudioClip song; //Qual música você quer que toque ao iniciar a tela?

    void Start()
    {
       SoundManager.instance.PlayBackgroundMusic(song);
    }

    public void LoadScene(string scene)
    {
        GameManager.instance.LoadSceneWithFade(scene);
    }

}
