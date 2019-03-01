using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScene : MonoBehaviour
{
    public string[] dialogos;
    public Texture texture;

    void Start()
    {
        TextPanelController.chatEnd += ChatEnded;
        TextPanelController.CreateDialogBox(dialogos, texture);
    }

    public void ChatEnded()
    {
        GameManager.instance.LoadSceneWithFade("scene01");
    }
}
