using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ola
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    GameManager gameManager;
    SoundManager soundManager;
    AnimationManager animManager;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeManagers()
    {
        GameObject videoManager, gameManager, animManager, soundManager;
        
        videoManager = Resources.Load("Prefabs/VideoManager") as GameObject;
        gameManager = Resources.Load("Prefabs/GameManager") as GameObject;
        animManager = Resources.Load("Prefabs/AnimationManager") as GameObject;
        soundManager = Resources.Load("Prefabs/SoundManager") as GameObject;

        if (VideoManager.instance == null)
        {
            Instantiate(videoManager);
        }

        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        if(AnimationManager.instance == null)
        {
            Instantiate(animManager);
        }

        if(SoundManager.instance == null)
        {
            Instantiate(soundManager);
        }
    }

    private void Awake()
    {
        if(instance == null)
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
    /// Função para carregar uma nova fase com fade - em construção
    /// </summary>
    /// <param name="Scene"></param>
    public void LoadSceneWithFade(string scene)
    {
        animManager = AnimationManager.instance;
        soundManager = SoundManager.instance;
        //SilabaControl silabaControl = SilabaControl.instance;
        //silabaControl.StopCoroutines();
        //StopAllCoroutines();
        soundManager.StopAllSounds();
        animManager = GameObject.FindGameObjectWithTag("AnimationManager").GetComponent<AnimationManager>();
        StartCoroutine(animManager.Fade(scene));
    }   
}
