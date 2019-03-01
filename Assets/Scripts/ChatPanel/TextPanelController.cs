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

    public delegate void ChatEnded();
    public static event ChatEnded chatEnd;

    public TextPanelController() { }

    private void Start()
    {
        tmpro = GameObject.Find("TextMeshPro Text").GetComponent<TextMeshProUGUI>();
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
    /// Esta função cria a caixa sobre o canvas da cena atual, portanto não bloqueia a tela,
    /// mas desativa todos colliders2D.
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
            int visibleCount = counter % (totalVisibleChars + 1);
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
        chatEnd();
        Destroy(gameObject);
    }
}
