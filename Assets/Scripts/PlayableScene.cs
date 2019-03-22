using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Utilizado para ser colocado em todas "cenas jogáveis". Ele verifica e instancia o xixi e o cocô do animal, por exemplo.
/// </summary>
public class PlayableScene : MonoBehaviour
{

    List<Vector3> peeList;
    List<Vector3> poopList;

    private void Start()
    {
        VerifyPoop();
        VerifyPee();
    }

    private void VerifyPoop()
    {
        poopList = SaveManager.instance.player.poopLocation.View(SceneManager.GetActiveScene().name);
        GameObject poop = Resources.Load("Prefabs/Items/Poop") as GameObject;
        foreach (Vector3 position in poopList)
        {
            GameObject newPoop = Instantiate(poop, position, Quaternion.identity);
            OnOff(newPoop);
        }
    }

    private void VerifyPee()
    {
        peeList = SaveManager.instance.player.peeLocation.View(SceneManager.GetActiveScene().name);
        GameObject pee = Resources.Load("Prefabs/Items/Pee") as GameObject;
        foreach (Vector3 position in peeList)
        {
            Instantiate(pee, position, Quaternion.identity);
        }
    }

    private void OnOff(GameObject obj)
    {
        obj.GetComponentInChildren<BoxCollider2D>().enabled = false;
        obj.GetComponentInChildren<BoxCollider2D>().enabled = true;
    }
}
