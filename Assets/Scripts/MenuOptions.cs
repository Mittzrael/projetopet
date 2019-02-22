using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    SoundManager soundManager;
    public AudioClip background;
    Text textoBotaoMusica;

    private void Awake()
    {
        textoBotaoMusica = GameObject.Find("Musica").GetComponent<UnityEngine.UI.Button>().GetComponentInChildren<UnityEngine.UI.Text>();
        soundManager = SoundManager.instance;
    }

    public void Start()
    {
        if (soundManager.IsBackgroundPlaying())
        {
            textoBotaoMusica.text = "Música: Ligado";
        }
        else
        {
            textoBotaoMusica.text = "Música: Desligado";
        }
    }

    public void MuteBackground()
    {
        if (soundManager.IsBackgroundPlaying())
        {
            soundManager.StopBackgroundMusic();
            textoBotaoMusica.text = "Música: Desligado";
        }
        else
        {
            soundManager.PlayBackgroundMusic(background);
            textoBotaoMusica.text = "Música: Ligado";
        }
    }

    public void CloseMenu()
    {
        Destroy(this.gameObject);
    }
}
