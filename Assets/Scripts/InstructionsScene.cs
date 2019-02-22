using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScene : MonoBehaviour
{
    GameObject chatPanel;
    // Start is called before the first frame update
    void Start()
    {
        chatPanel = Resources.Load("Prefabs/ChatPanel") as GameObject;
        GameObject newChatPanel;
        newChatPanel = Instantiate(chatPanel);
        newChatPanel.transform.SetParent(GameObject.Find("Canvas").transform, false);
        newChatPanel.transform.position = new Vector3(205,75,0);
        newChatPanel.GetComponent<TextPanelController>().textString = new string[3];
        string[] chat = newChatPanel.GetComponent<TextPanelController>().textString;
        chat[0] = "Fuck You.";
        chat[1] = "I said Fuck You.";
        chat[2] = "Fuck You maggot.";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
