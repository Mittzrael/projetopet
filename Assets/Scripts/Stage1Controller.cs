using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Controller : MonoBehaviour
{
    public string[] dialogoInicial;
    public Texture texture;
    public string[] dialogoAindaTemErros;

    // Start is called before the first frame update
    void Start()
    {
        TextPanelController.CreateDialogBox(dialogoInicial, new Vector3(205, 75, 0), texture);
    }

    public void VerificarErros()
    {
        TextPanelController.CreateDialogBox(dialogoAindaTemErros, new Vector3(205, 75, 0), texture);
    }
}
