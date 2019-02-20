using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource backgroundAudioSource;
    private AudioSource sfxAudioSource;
    private AudioSource animalAudioSource;

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
        DontDestroyOnLoad(gameObject);
    }

    public void SoundManagerOnTransition()
    {
        backgroundAudioSource.Stop();
        sfxAudioSource.Stop();
        animalAudioSource.Stop();
    }

    /* Fazer função para mudar o volume.
     * Função de cada play, recebendo um audioClip
     */
}
