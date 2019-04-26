using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool petDoingAction = false; //Sempre que o pet realizar uma ação, seta para true;
    private bool blockSwipe = false; //Bloqueia o CameraSwipe

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
    public bool petAlreadyInstantiate = false;
    public PetList petList;

    #region Loader
    private GameManager gameManager;
    private SoundManager soundManager;
    private AnimationManager animManager;
    private SaveManager saveManager;
    private TimeManager timeManager;

    public bool BlockSwipe { get => blockSwipe; set => blockSwipe = value; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeManagers()
    {
        GameObject gameManager, animManager, soundManager, saveManager, timeManager;
        
        gameManager = Resources.Load("Prefabs/GameManager") as GameObject;
        animManager = Resources.Load("Prefabs/AnimationManager") as GameObject;
        soundManager = Resources.Load("Prefabs/SoundManager") as GameObject;
        saveManager = Resources.Load("Prefabs/SaveManager") as GameObject;
        timeManager = Resources.Load("Prefabs/TimeManager") as GameObject;

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

        if (TimeManager.instance == null)
        {
            Instantiate(timeManager);
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
        StartCoroutine(StartProcess());
    }

    /// <summary>
    /// Função que inicia a contagem de tempo no jogo.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartProcess()
    {
        yield return new WaitUntil(() => (TimeManager.instance != null));
        if (SaveManager.instance.player.flag[0].state)
        {
            TimeManager.instance.PeriodProcess();
        }
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

    /// <summary>
    /// Função que é chamada no início de cada novo período de tempo.
    /// </summary>
    public void StartPeriod()
    {
        TimeManager timeManager = TimeManager.instance;

        PopUpWarning.instance.CallAllWarnings(timeManager.GetCurrentPeriod());
        StartCoroutine(PetBasicAI.instance.CallPeePoop());
        PetBasicAI.instance.RestartDrinkCount();

        SaveManager.instance.player.health.PutInCleanFoodPot(true);
        SaveManager.instance.player.health.PutInCleanWaterPot(true);

        ///Coisas que acontecem no começo do período
        Debug.Log("Está começando o período: " + timeManager.GetCurrentPeriod().ToString());
    }

    private void OnApplicationQuit() //Usando para salvar a data em que o jogador fecha o aplicativo
    {
        if (SaveManager.instance.player.flag[0].state)
        {
            SaveManager.instance.player.lastTimePlayed = System.DateTime.UtcNow.ToString();
            SaveManager.instance.Save();
        }
    }
}