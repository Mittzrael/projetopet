using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkingBallon : MonoBehaviour
{
    public Sprite content;

    /// <summary>
    /// Cria um pensamento para o gameObject thinker, com o conteúdo do pensamento
    /// </summary>
    /// <param name="thinker">GameObject que pensará</param>
    /// <param name="content">Conteúdo do balão de pensamento, em uma string com o nome da imagem que irá aparecer</param>
    public static void CreateThinking(GameObject thinker, string content)
    {
        GameObject thinkingBallon = Resources.Load("Prefabs/ThinkingBallon") as GameObject;
        Vector3 position = new Vector3(thinker.transform.position.x + thinker.GetComponent<Renderer>().bounds.size.x/2, thinker.transform.position.y + thinker.GetComponent<Renderer>().bounds.size.y/2, thinker.transform.position.z);
        thinkingBallon.GetComponent<ThinkingBallon>().content = Resources.Load<Sprite>(string.Concat("Images/", content));
        thinkingBallon = Instantiate(thinkingBallon, position, Quaternion.identity);
    }

    private void Awake()
    {
        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = content;
        gameObject.GetComponent<Animator>().SetBool("AnimationOn", true);
        StartCoroutine(DestroyBallon());
    }

    private IEnumerator DestroyBallon()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }
}