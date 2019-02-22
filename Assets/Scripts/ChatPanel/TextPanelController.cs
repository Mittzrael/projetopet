using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanelController : MonoBehaviour {

    [Tooltip("Quantidade de mensagens até fechar janela. Mensagens a cada aperto de \"Next\"")]
    public string[] textString;
    private Text textComponent;
    private Button nextButton;
    private Button closeButton;
    private Animator charImageAnimator;
    private Animator textAnimator;
    private Animator panelAnimator;
    private int stringIndex = 0;
   // [Tooltip("Imagem do personagem que vai aparecer à esquerda do quadro.")]
   // public Sprite charSprite;
    [Tooltip("Material do painel de fundo.")]
    public Material panelMaterial;

    private void Start()
    {
      //  GameObject.Find("CharacterImage").GetComponent<SpriteRenderer>().sprite = charSprite;
        GameObject.Find("PanelBackground").GetComponent<Image>().material = panelMaterial;
        textComponent = GameObject.Find("Text").GetComponentInChildren<UnityEngine.UI.Text>();
        nextButton =  GameObject.Find("NextButton").GetComponentInChildren<UnityEngine.UI.Button>();
        closeButton = GameObject.Find("CloseButton").GetComponentInChildren<UnityEngine.UI.Button>();
        charImageAnimator = GameObject.Find("CharacterImage").GetComponent<Animator>();
        textAnimator = textComponent.GetComponent<Animator>();
        panelAnimator = GameObject.Find("PanelBackground").GetComponent<Animator>();
        StartCoroutine(InitialAnimations()); 
    }

    IEnumerator InitialAnimations()
    {               
        yield return new WaitForSeconds(0.4f);
        charImageAnimator.SetBool("PanelAnimationDone", true);
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(PlayText(textString[0]));
    }

    IEnumerator PlayText(string textToPlay)
    {       
        int i = 0;
        textComponent.text = "";
        while (i < textToPlay.Length)
        {
            nextButton.interactable = false;
            textComponent.text += textToPlay[i++];
            yield return new WaitForSeconds(0.05f);
        }
       
        stringIndex++;
        if (textString.Length == stringIndex )
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

    public void NextText()
    {
        string nextText;
        nextText = textString[stringIndex];
        StartCoroutine(PlayText(nextText));
    }   

    public void ClosePanel()
    {
        StartCoroutine(EndingAnimations());        
    }

    IEnumerator EndingAnimations()
    {
        closeButton.interactable = false;
        textAnimator.SetBool("TextVanish", true);
        yield return new WaitForSeconds(0.3f);
        charImageAnimator.SetBool("EndingAnimation", true);
        yield return new WaitForSeconds(0.65f);
        panelAnimator.SetBool("PanelVanish", true);
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
