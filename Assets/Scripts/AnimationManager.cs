using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationManager : MonoBehaviour {

    public static AnimationManager instance = null;
    public AudioClip audio;

    private Animator animator;
    private GameObject animForward, animBackward, fade, animation;

    /// <summary>
    /// Garante a unicidade do manager e inicia as animações
    /// </summary>
    public void Awake()
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

        // Carrega as animações
        animForward = Resources.Load("Prefabs/Animations/HyperspaceForward") as GameObject;
        animBackward = Resources.Load("Prefabs/Animations/HyperspaceBackwards") as GameObject;
        fade = Resources.Load("Prefabs/Animations/Fade") as GameObject;

        // Instancia as animações carregadas
        animForward = Instantiate(animForward);
        animBackward = Instantiate(animBackward);
        fade = Instantiate(fade);

        // Coloca as animações como filhas do AnimationManager
        animForward.transform.SetParent(GameObject.FindGameObjectWithTag("AnimationManager").transform, false);
        animBackward.transform.SetParent(GameObject.FindGameObjectWithTag("AnimationManager").transform, false);
        fade.transform.SetParent(GameObject.FindGameObjectWithTag("AnimationManager").transform, false);
    }

    /// <summary>
    /// Decide qual animação tocar a partir do parametro levelComplete
    /// True - toca a animação de avanço
    /// False - Toca a animação de retorno
    /// </summary>
    /// <param name="levelComplete"></param>
    public void PlayTransitionSceneAnimation(bool levelComplete, string levelName)
    {
        if (levelName.Contains("stageSelect"))
        {
            if (levelComplete)
            {
                animation = animForward;
            }
            else
            {
                animation = animBackward;
            }
            SoundManager.instance.PlaySfx(audio);
        }
        else
        {
            animation = fade;
        }

        animator = animation.GetComponent<Animator>();
        animator.SetTrigger("FadeStart");
    }

    /// <summary>
    /// Toca animação simples de FadeIn e FadeOut 
    /// </summary>
    public void PlaySimpleTrasitionAnimation()
    {
        fade.GetComponent<Animator>().SetTrigger("FadeStart");
    }

    /// <summary>
    /// Controle da animação de FadeIn e FadeOut
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    public IEnumerator Fade(string scene)
    {
        AnimationManager.instance.PlaySimpleTrasitionAnimation();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
    }
}
