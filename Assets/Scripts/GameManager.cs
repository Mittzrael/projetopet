using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public bool petDoingAction = false; //Sempre que o pet realizar uma ação, seta para true;
    private bool blockSwipe = false; //Bloqueia o CameraSwipe

#region Loader
    private GameManager gameManager;
    private SoundManager soundManager;
    private AnimationManager animManager;
    private SaveManager saveManager;

    public bool BlockSwipe { get => blockSwipe; set => blockSwipe = value; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeManagers()
    {
        GameObject videoManager, gameManager, animManager, soundManager, saveManager;
        
        videoManager = Resources.Load("Prefabs/VideoManager") as GameObject;
        gameManager = Resources.Load("Prefabs/GameManager") as GameObject;
        animManager = Resources.Load("Prefabs/AnimationManager") as GameObject;
        soundManager = Resources.Load("Prefabs/SoundManager") as GameObject;
        saveManager = Resources.Load("Prefabs/SaveManager") as GameObject;

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

        if(SaveManager.instance == null)
        {
            Instantiate(saveManager);
        }
    }
    #endregion

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
        //soundManager.StopAllSounds();
        animManager = GameObject.FindGameObjectWithTag("AnimationManager").GetComponent<AnimationManager>();
        StartCoroutine(animManager.Fade(scene));
    }

    private void OnApplicationQuit()
    {
        SaveManager.instance.Load(0);
        System.DateTime nowTime = System.DateTime.UtcNow; //Data atual
        System.TimeSpan timeElapsed = nowTime - System.Convert.ToDateTime(SaveManager.instance.player.lastTimePlayed); //Tempo atual - tempo da última vez que foi jogado
        SaveManager.instance.player.lastTimePlayed = System.DateTime.UtcNow.ToString(); //Salva o tempo atual como string para o SaveManager
        SaveManager.instance.Save();
    }
}