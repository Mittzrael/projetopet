using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Controller : MonoBehaviour
{
    public Texture texture;
    public string[] dialogoInicial;
    public string[] dialogoAindaTemErros;
    public string[] dialogoTudoCerto;
    private bool somethingStillNotFixed = true;
    private bool somethingStillNotClean = true;
    private GameObject[] fixables; //set tag fixables in objects
    private GameObject[] cleanables; //set tag cleanables in objects
    public string nextScene;

    public GameObject objetos;

    void Start()
    {
        TextPanelController.CreateDialogBox(dialogoInicial, texture);
    }

    public void VerificarErros()
    {
        fixables = GameObject.FindGameObjectsWithTag("fixables");
        cleanables = GameObject.FindGameObjectsWithTag("cleanables");

        foreach (GameObject objeto in fixables)//se houver algum false, set somethingStillNotFixed = true e sai do loop
        {
            if (objeto.GetComponent<ChangeSprite>().isFixed == true)
            {
                somethingStillNotFixed = false;
            }
            else
            {
                somethingStillNotFixed = true;
                break;
            }
        }

        foreach (GameObject objeto in cleanables)//se houver algum false, set somethingStillNotCleaned = true e sai do loop
        {
            if (objeto.GetComponent<DeactivateOnClick>().isCleaned == true)
            {
                somethingStillNotClean = false;
            }
            else
            {
                somethingStillNotClean = true;
                break;
            }
        }

        if (somethingStillNotFixed == true || somethingStillNotClean == true)
        {
            TextPanelController.CreateDialogBox(dialogoAindaTemErros, texture);
        }
        else
        {
            TextPanelController.ChatEndNotification += ErrosCorrigidos;//subscribe to event (avisa ao terminar os dialogos)
            TextPanelController.CreateDialogBox(dialogoTudoCerto, texture);
        }                
    }

    public void ErrosCorrigidos()
    {
        TextPanelController.ChatEndNotification -= ErrosCorrigidos;//unsubscribe from event (para de ouvir o evento, evitar sobrecarga desnecessária)
        GameManager.instance.LoadSceneWithFade(nextScene);
    }
}
