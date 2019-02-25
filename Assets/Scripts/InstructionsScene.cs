using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScene : MonoBehaviour
{
    public string[] dialogos;
    public Texture texture;

    void Start()
    {
        TextPanelController.CreateDialogBox(dialogos, new Vector3(205, 75, 0), texture);
    }
}
