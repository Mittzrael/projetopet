using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TextPanelController : MonoBehaviour {

    private TextMeshProUGUI tmpro;
    public string[] textString;
    private Button nextButton;
    private Button closeButton;
    private Animator charImageAnimator;
    private Animator tmproAnimator;
    private Animator panelAnimator;
    private int stringIndex = 0;
    public Texture charImage;
    private bool screenClicked = false;
    GameObject panelBlocker;
    int visibleCount;

    public delegate void ChatEnded();//evento para avisar que terminou o dialogo
    public static event ChatEnded ChatEndNotification;//evento para avisar que terminou o dialogo

    private void Start()
    {
        tmpro = GameObject.Find("TextMeshPro Text").GetComponent<TextMeshProUGUI>();
        panelBlocker = GameObject.Find("PanelCanvasBlocker");
        GameObject.Find("CharacterImage").GetComponent<RawImage>().texture = charImage;
        nextButton = GameObject.Find("NextButton").GetComponent<UnityEngine.UI.Button>();
        closeButton = GameObject.Find("CloseButton").GetComponent<UnityEngine.UI.Button>();
        charImageAnimator = GameObject.Find("CharacterImage").GetComponent<Animator>();
        tmproAnimator = tmpro.GetComponent<Animator>();
        panelAnimator = GameObject.Find("PanelBackground").GetComponent<Animator>();
        StartCoroutine(InitialAnimations());
    }

    /// <summary>
    /// Chama a caixa de diálogo estilo RPG, com uma string de textos, na posição indicada.
    /// Destroi objeto ao finalizar.
    /// Esta função cria a caixa como filho do canvas da cena atual, portanto bloqueia os elementos UI 
    /// e desativa todos colliders2D.
    /// </summary>
    /// <param name="dialogs">Vetor String dos dialogos.</param>
    /// <param name="charImage">Texture da imagem que vai aparecer no canto da caixa.</param>
    public static void CreateDialogBox(string[] dialogs, Texture charImage)
    {
        Collider2D[] Cols;
        Cols = FindObjectsOfType<Collider2D>();
        foreach (Collider2D c in Cols)
        {
            c.enabled = false;
        }

        GameObject dialogPanel = Resources.Load("Prefabs/ChatPanel") as GameObject;
        TextPanelController dialogBox = dialogPanel.GetComponent<TextPanelController>();
        dialogBox.textString = dialogs;
        dialogBox.charImage = charImage;        
        Instantiate(dialogPanel).transform.SetParent(GameObject.Find("Canvas").transform, false);
    }

    /// <summary>
    /// Chama a caixa de diálogo estilo RPG, com uma string de textos, na posição indicada, SEM bloquear a tela.
    /// Destroi objeto ao finalizar.
    /// Esta função cria a caixa dentro do canvas da cena atual, mas não bloqueia a tela nem
    /// desativa todos colliders2D caso allowIpunt = true.
    /// </summary>
    /// <param name="dialogs">Vetor String dos dialogos.</param>
    /// <param name="charImage">Texture da imagem que vai aparecer no canto da caixa.</param>
    /// <param name="allowInput">Bool, true para não bloquear a tela.</param>
    public static void CreateDialogBox(string[] dialogs, Texture charImage, bool allowInput)
    {       
        GameObject dialogPanel = Resources.Load("Prefabs/ChatPanel") as GameObject;
        TextPanelController dialogBox = dialogPanel.GetComponent<TextPanelController>();
        dialogBox.textString = dialogs;
        dialogBox.charImage = charImage;
        Instantiate(dialogPanel).transform.SetParent(GameObject.Find("Canvas").transform, false);

        if (allowInput)
        {
            GameObject.Find("PanelCanvasBlocker").SetActive(false);
        }
        else
        {
            Collider2D[] Cols;
            Cols = FindObjectsOfType<Collider2D>();
            foreach (Collider2D c in Cols)
            {
                c.enabled = false;
            }
        }
    }

    private void OnMouseUpAsButton()//verifica se foi clicado na tela para avançar o texto rápido
    {
        if (nextButton.interactable == false && closeButton.interactable == false)
        {
            screenClicked = true;
        }
        else if (nextButton.interactable == true)
        {
            NextText();
        }
        else if (closeButton.interactable == true)
        {
            ClosePanel();
        }        
    }

    /// <summary>
    /// Reproduz as animações iniciais e chama a função que exibe o texto.
    /// </summary>
    /// <returns></returns>
    IEnumerator InitialAnimations()
    {               
        yield return new WaitForSeconds(0.35f);
        charImageAnimator.SetBool("PanelAnimationDone", true);
        yield return new WaitForSeconds(0.35f);
        StartCoroutine(PlayTextMeshProText(textString[0]));
    }

    /// <summary>
    /// Reproduz o texto um caractere por vez e ativa o botão NEXT ao finalizar,
    /// ou CLOSE caso seja o último elemento do vetor.
    /// </summary>
    /// <param name="textToPlay"></param>
    /// <returns></returns>
    IEnumerator PlayTextMeshProText(string textToPlay)
    {
        tmpro.text = textToPlay;
        int totalVisibleChars = tmpro.text.Length;
        int counter = 0;

        while (true)
        {
            nextButton.interactable = false;
            visibleCount = counter % (totalVisibleChars + 1);
            if (screenClicked)
            {
                screenClicked = false;
                visibleCount = totalVisibleChars;
            }            
            tmpro.maxVisibleCharacters = visibleCount;
            if (visibleCount >= totalVisibleChars)
            {
                break;
            }
            counter += 1;
            yield return new WaitForSeconds(0.02f);//tempo entre mostrar cada letra
        }

        stringIndex++;
        if (textString.Length == stringIndex)
        {
            nextButton.interactable = false;
            closeButton.interactable = true;
            closeButton.transform.SetSiblingIndex(10);
        }
        else
        {
            nextButton.interactable = true;
        }
    }

    /// <summary>
    /// Chama o próximo elemento do vetor de strings.
    /// </summary>
    public void NextText()
    {
        string nextText;
        nextText = textString[stringIndex];
        StartCoroutine(PlayTextMeshProText(nextText));
    }
   
    /// <summary>
    /// Finaliza com animações e destroi objeto.
    /// Reativa todos colliders;
    /// </summary>
    public void ClosePanel()
    {
        StartCoroutine(EndingAnimations());        
    }

    IEnumerator EndingAnimations()
    {
        closeButton.interactable = false;
        tmproAnimator.SetBool("TextVanish", true);
        yield return new WaitForSeconds(0.25f);
        charImageAnimator.SetBool("EndingAnimation", true);
        yield return new WaitForSeconds(0.5f);
        panelAnimator.SetBool("PanelVanish", true);
        yield return new WaitForSeconds(0.35f);
        Collider2D[] Cols;
        Cols = FindObjectsOfType<Collider2D>();

        foreach (Collider2D c in Cols)
        {
            c.enabled = true;
        }

        if (ChatEndNotification != null)//verifica se tem alguém ouvindo o evento (se não tiver, é null)
        {
            ChatEndNotification();//avisa sobre o evento (terminou o diálogo)
        }        

        Destroy(gameObject);
    }
}
