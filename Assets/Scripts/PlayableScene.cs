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
    GameObject petFather;
    string currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        VerifyScene();
    }

    private void VerifyScene()
    {
        VerifyPoop();
        VerifyPee();
        VerifyPot();
        petFather = GameObject.Find("PetFather");
        petFather.GetComponent<Invisible>().StatusVerify();
    }

    private void VerifyPoop()
    {
        poopList = SaveManager.instance.player.poopLocation.View(SceneManager.GetActiveScene().name);
        GameObject poop = Resources.Load("Prefabs/Items/Poop") as GameObject;
        foreach (Vector3 position in poopList)
        {
            GameObject newPoop = Instantiate(poop, position, Quaternion.identity);
            OnOff(newPoop); //Talvez tirar no futuro, vê aí
        }
    }

    private void VerifyPot()
    {
        if (SaveManager.instance.player.foodPotLocation.sceneName.Equals(currentScene))
        {
            Debug.Log("Entrou food");
            Instantiate(Resources.Load("Prefabs/Items/PotFood") as GameObject, SaveManager.instance.player.foodPotLocation.elementPosition, Quaternion.identity);
        }

        if (SaveManager.instance.player.waterPotLocation.sceneName.Equals(currentScene))
        {
            Debug.Log("Entrou water");
            Instantiate(Resources.Load("Prefabs/Items/PotWater") as GameObject, SaveManager.instance.player.waterPotLocation.elementPosition, Quaternion.identity);
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
