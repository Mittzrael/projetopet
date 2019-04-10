using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool petDoingAction = false; //Sempre que o pet realizar uma ação, seta para true;
    private bool blockSwipe = false; //Bloqueia o CameraSwipe
    [SerializeField]
    private float decreaseRate = -0.01f;
    [SerializeField]
    private double decreaseTimeHappiness = 30;
    [SerializeField]
    private double decreaseTimeHungry = 20;
    [SerializeField]
    private double decreaseTimeThirsty = 50;
    [SerializeField]
    private double decreaseTimeHygiene = 80;

    // Localização das portas que pet tem acesso
    public float doorKitchenToYard = -2700;
    public float doorYardToKitchen = 2400;

    // Localização dos itens que o pet tem acesso
    public string foodScene;
    public float foodPosition;
    public string waterScene;
    public float waterPosition;
    public string bathroomScene;
    public float bathroomPosition;
    public string[] petAccessScenes;

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

    private void Start()
    {
        SaveManager.instance.Load(0);
        DecreaseStats();
    }

    #region Diminuição de atributos do Health de acordo com o tempo

    /// <summary>
    /// Diminui os atributos de acordo com o tempo em que o jogador ficou sem entrar no jogo.
    /// </summary>

    private void DecreaseStats()
    {
        TimeOffDecrease();
        StartCoroutine(DecreaseHappiness(decreaseTimeHappiness));
        //StartCoroutine(DecreaseHungry(decreaseTimeHungry));
        StartCoroutine(DecreaseThirsty(decreaseTimeThirsty));
        StartCoroutine(DecreaseHygiene(decreaseTimeHygiene));
    }

    private void TimeOffDecrease()
    {
        double timeElapsed = TimeSinceYouPlayed.TimeElapsedSeconds();
        Health health = SaveManager.instance.player.health;

        int timeElapsedForAttribute = (int)(timeElapsed/decreaseTimeHappiness);
        health.PutInHappiness(decreaseRate * timeElapsedForAttribute);

        timeElapsedForAttribute = (int)(timeElapsed / decreaseTimeHungry);
        //health.PutInHungry(decreaseRate * timeElapsedForAttribute);

        timeElapsedForAttribute = (int)(timeElapsed / decreaseTimeThirsty);
        health.PutInThirsty(decreaseRate * timeElapsedForAttribute);

        timeElapsedForAttribute = (int)(timeElapsed / decreaseTimeHygiene);
        health.PutInHygiene(decreaseRate * timeElapsedForAttribute);
    }

    /*
    /// <summary>
    /// Diminui a fome de acordo com o valor do decreaseRate e chama recursivamente ela após s segundos
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private IEnumerator DecreaseHungry(double s)
    {
        SaveManager.instance.player.health.PutInHungry(decreaseRate);
        yield return new WaitForSeconds((float)s);
        StartCoroutine(DecreaseHungry(s));
    }
    */

    /// <summary>
    /// Diminui a sede de acordo com o valor do decreaseRate e chama recursivamente ela após s segundos
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private IEnumerator DecreaseThirsty(double s)
    {
        SaveManager.instance.player.health.PutInThirsty(decreaseRate);
        yield return new WaitForSeconds((float)s);
        StartCoroutine(DecreaseThirsty(s));
    }

    /// <summary>
    /// Diminui a felicidade de acordo com o valor do decreaseRate e chama recursivamente ela após s segundos
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private IEnumerator DecreaseHappiness(double s)
    {
        SaveManager.instance.player.health.PutInHappiness(decreaseRate);
        yield return new WaitForSeconds((float)s);
        StartCoroutine(DecreaseHappiness(s));
    }

    /// <summary>
    /// Diminui a higiene de acordo com o valor do decreaseRate e chama recursivamente ela após s segundos
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private IEnumerator DecreaseHygiene(double s)
    {
        SaveManager.instance.player.health.PutInHygiene(decreaseRate);
        yield return new WaitForSeconds((float)s);
        StartCoroutine(DecreaseHygiene(s));
    }

    #endregion

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

    private void OnApplicationQuit() //Usando para salvar a data em que o jogador fecha o aplicativo
    {
        SaveManager.instance.player.lastTimePlayed = System.DateTime.UtcNow.ToString(); //Salva o tempo atual como string para o SaveManager
        if (SaveManager.instance.player.flag[0].state)
        {
            SaveManager.instance.Save();
        }
    }
}