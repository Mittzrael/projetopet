using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField]
    private AudioSource backgroundAudioSource = null;
    [SerializeField]
    private AudioSource sfxAudioSource = null;
    [SerializeField]
    private AudioSource animalAudioSource = null;

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

    /// <summary>
    /// Stops all music and sounds currently being reproduced.
    /// </summary>
    public void StopAllSounds()
    {
        backgroundAudioSource.Stop();
        sfxAudioSource.Stop();
        animalAudioSource.Stop();
    }

    /// <summary>
    /// Plays a background music.
    /// </summary>
    /// <param name="backgroundMusic">AudioClip to be reproduced.</param>
    public void PlayBackgroundMusic(AudioClip backgroundMusic)
    {
        backgroundAudioSource.clip = backgroundMusic;
        backgroundAudioSource.Play();
    }

    /// <summary>
    /// Plays a Sound Effect.
    /// </summary>
    /// <param name="sfx">AudioClip to be reproduced.</param>
    public void PlaySFX(AudioClip sfx)
    {
        sfxAudioSource.clip = sfx;
        sfxAudioSource.Play();
    }

    /// <summary>
    /// Plays a sound effect for animal sounds.
    /// </summary>
    /// <param name="animalSound">AudioClip to be reproduced.</param>
    public void PlayAnimalSound(AudioClip animalSound)
    {
        animalAudioSource.clip = animalSound;
        animalAudioSource.Play();
    }

    /// <summary>
    /// Sets MUTE = true to background music.
    /// </summary>
    public void MuteBackgroundMusic()
    {
        backgroundAudioSource.mute = true;
    }

    /// <summary>
    /// Sets MUTE = true to all sfx.
    /// </summary>
    public void MuteSFX()
    {
        sfxAudioSource.mute = true;
        animalAudioSource.mute = true;
    }

    /// <summary>
    /// Changes background music volume.
    /// </summary>
    /// <param name="volume">Float 0 to 1.</param>
    public void ChangeBackgroundVolume(float volume)
    {
        backgroundAudioSource.volume = volume;
    }

    /// <summary>
    /// Stops background music.
    /// </summary>
    public void StopBackgroundMusic()
    {
        backgroundAudioSource.Stop();
    }

    /// <summary>
    /// Stops SFX.
    /// </summary>
    public void StopAnimalSound()
    {
        animalAudioSource.Stop();
    }

    /// <summary>
    /// Stops SFX for animal sounds.
    /// </summary>
    public void StopSFXSound()
    {
        sfxAudioSource.Stop();
    }

    /// <summary>
    /// Check if there is any background music currently playing.
    /// </summary>
    /// <returns>Returns true if something is being played or false if nothing is playing.</returns>
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
}
