using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    public GameObject pensante;
    // Start is called before the first frame update
    void Start()
    {
        ThinkingBallon.CreateThinking(pensante, "Happiness");
    }
}
