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

    public void StopAllSounds()
    {
        backgroundAudioSource.Stop();
        sfxAudioSource.Stop();
        animalAudioSource.Stop();
    }

    public void PlayBackgroundMusic(AudioClip backgroundMusic)
    {
        backgroundAudioSource.clip = backgroundMusic;
        backgroundAudioSource.Play();
    }

    public void PlaySFX(AudioClip sfx)
    {
        sfxAudioSource.clip = sfx;
        sfxAudioSource.Play();
    }

    public void PlayAnimalSound(AudioClip animalSound)
    {
        animalAudioSource.clip = animalSound;
        animalAudioSource.Play();
    }

    public void MuteBackgroundMusic()
    {
        backgroundAudioSource.mute = true;
    }

    public void MuteSFX()
    {
        sfxAudioSource.mute = true;
        animalAudioSource.mute = true;
    }

    public void ChangeBackgroundVolume(float volume)
    {
        backgroundAudioSource.volume = volume;
    }

    public void StopBackgroundMusic()
    {
        backgroundAudioSource.Stop();
    }

    public void StopAnimalSound()
    {
        animalAudioSource.Stop();
    }

    public void StopSFXSound()
    {
        sfxAudioSource.Stop();
    }

    public bool IsBackgroundPlaying()
    {
        if (backgroundAudioSource.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /* Fazer função para mudar o volume.
     * Função de cada play, recebendo um audioClip
     */
}
