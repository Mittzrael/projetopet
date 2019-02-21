using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private SoundManager soundManager;
    [Tooltip("Música para tocar.")]
    public AudioClip song; //Qual música você quer que toque ao iniciar a tela?

    void Start()
    {
        soundManager = SoundManager.instance;
        soundManager.PlayBackgroundMusic(song);
    }
}
