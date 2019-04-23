using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Utilizado para ser colocado em todas "cenas jogáveis". Ele verifica e instancia o xixi e o cocô do animal, por exemplo.
/// </summary>
public class PlayableScene : MonoBehaviour
{

    private List<Vector3> peeList;
    private List<Vector3> poopList;
    private GameObject petFather;
    private string currentScene;
    [SerializeField]
    private PetList petList;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        InstantiatePet();
        VerifyScene();
    }

    public void InstantiatePet()
    {
        if (!GameManager.instance.petAlreadyInstantiate)
        {
            Debug.Log("Instancio o pet número: " + SaveManager.instance.player.petSelecionado);
            Instantiate(petList.pet[SaveManager.instance.player.petSelecionado].petGameObject);
            GameManager.instance.petAlreadyInstantiate = true;
        }
    }

    /// <summary>
    /// Função chamada no Start, que chama a verificação de todos os elementos que usam dela e chama o StatusVerify do pet.
    /// </summary>
    private void VerifyScene()
    {
        VerifyPoop();
        VerifyPee();
        VerifyPot();
        petFather = GameObject.FindGameObjectWithTag("PetFather");
        petFather.GetComponent<Invisible>().StatusVerify();
    }

    /// <summary>
    /// Verifica se deveria existir algum poop na scene, e os instância.
    /// </summary>
    private void VerifyPoop()
    {
        poopList = SaveManager.instance.player.poopLocation.View(SceneManager.GetActiveScene().name);
        GameObject poop = Resources.Load("Prefabs/Items/Poop") as GameObject;
        foreach (Vector3 position in poopList) //Verifica todos os poop na poopList e instancia na posição marcada o poop.
        {
            GameObject newPoop = Instantiate(poop, position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Verifica se deveria existir algum pote de comida na scene, e o instância.
    /// </summary>
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

    /// <summary>
    /// Verifica se deveria existir algum pee na scene, e os instância.
    /// </summary>
    private void VerifyPee()
    {
        peeList = SaveManager.instance.player.peeLocation.View(SceneManager.GetActiveScene().name);
        GameObject pee = Resources.Load("Prefabs/Items/Pee") as GameObject;
        foreach (Vector3 position in peeList) //Verifica todos os pee na peeList e instancia na posição marcada o pee.
        {
            Instantiate(pee, position, Quaternion.identity);
        }
    }

    private void StartCounters()
    {

    }
}
