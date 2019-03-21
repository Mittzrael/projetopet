using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Utilizado para ser colocado em todas "cenas jogáveis". Ele verifica e instancia o xixi e o cocô do animal, por exemplo.
/// </summary>
public class PlayableScene : MonoBehaviour
{

    List<Vector3> poopList;

    private void Start()
    {
        VerifyPoop();
    }

    private void VerifyPoop()
    {
        poopList = SaveManager.instance.player.poopLocation.View(SceneManager.GetActiveScene().name);
        GameObject poop = Resources.Load("Prefabs/Items/Poop") as GameObject;
        foreach(Vector3 position in poopList)
        {
            Instantiate(poop, position, Quaternion.identity);
        }
    }
}
