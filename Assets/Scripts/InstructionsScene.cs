using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScene : MonoBehaviour
{
    public string[] dialogos;
    public Texture texture;

    void Start()
    {
        TextPanelController.ChatEndNotification += ChatEnded;//subscribe to event (avisa ao terminar os dialogos)
        TextPanelController.CreateDialogBox(dialogos, texture);
    }

    public void ChatEnded()
    {
        TextPanelController.ChatEndNotification -= ChatEnded;//unsubscribe from event (para de ouvir o evento, evitar sobrecarga desnecessária)
        GameManager.instance.LoadSceneWithFade("scene01");
    }
}
