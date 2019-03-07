using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkingBallon : MonoBehaviour
{
    public Sprite content;

    public static void CreateThinking(GameObject thinker, string content)
    {
        Debug.Log("Entrou no CreateThiking");
        GameObject thinkingBallon = Resources.Load("Prefabs/ThinkingBallon") as GameObject;
        Vector3 position = new Vector3(thinker.transform.position.x + 340, thinker.transform.position.y + 340, thinker.transform.position.z);
        thinkingBallon.GetComponent<ThinkingBallon>().content = Resources.Load<Sprite>(string.Concat("Images/", content));
        thinkingBallon = Instantiate(thinkingBallon, position, Quaternion.identity);
        Debug.Log("Passou pelo CreateThiking");
    }

    private void Awake()
    {
        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = content;
        gameObject.GetComponent<Animator>().SetBool("AnimationOn", true);
    }
}