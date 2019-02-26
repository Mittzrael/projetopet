using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Controller : MonoBehaviour
{
    public string[] dialogoInicial;
    public Texture texture;
    public string[] dialogoAindaTemErros;
    public string[] dialogoTudoCerto;
    private bool somethingStillWrong = true;
    private GameObject[] fixables;

    void Start()
    {
        TextPanelController.CreateDialogBox(dialogoInicial, new Vector3(205, 75, 0), texture);
    }

    public void VerificarErros()
    {
        fixables = GameObject.FindGameObjectsWithTag("fixables");

        foreach (GameObject objeto in fixables)//se houver algum false, set somethingStillWrong = true e sai do loop
        {
            if (objeto.GetComponent<ChangeSprite>().isFixed == true)
            {
                somethingStillWrong = false;
            }
            else
            {
                somethingStillWrong = true;
                break;
            }           
        }

        if (somethingStillWrong == true)
        {
            TextPanelController.CreateDialogBox(dialogoAindaTemErros, new Vector3(205, 75, 0), texture);
        }
        else
        {
            TextPanelController.CreateDialogBox(dialogoTudoCerto, new Vector3(205, 75, 0), texture);
        }                
    }
}
