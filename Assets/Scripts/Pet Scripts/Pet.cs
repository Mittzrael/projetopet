using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Pet: MonoBehaviour
{
    public ElementLocation petCurrentLocation;
    private GameObject poop;
    private GameObject pee;
    private DogMitza petAnimationScript;

    public void Start()
    {
        petAnimationScript = gameObject.GetComponentInChildren<DogMitza>();
    }

    public ElementLocation GetPetLocation()
    {
        return petCurrentLocation;
    }

    public void SetPetLocation(ElementLocation newPetLocation)
    {
        petCurrentLocation = newPetLocation;
    }

    public void SetPetScene(string sceneName)
    {
        Debug.Log(sceneName);
        petCurrentLocation.sceneName = sceneName;
    }

    public IEnumerator Eat()
    {
        yield return new WaitForSeconds(0);
    }

    public void Eat(Food food)
    {
        SaveManager.instance.player.health.PutInHungry(food.GetNutrionalValor());
        Debug.Log("comi");
        SaveManager.instance.player.health.PutInPoop(food.GetNutrionalValor()/2);
    }

    public IEnumerator Drink()
    {
        yield return new WaitForSeconds(0);
    }

    public void Drink(Food food)
    {
        SaveManager.instance.player.health.PutInThirsty(food.GetNutrionalValor());
        SaveManager.instance.player.health.PutInPee(food.GetNutrionalValor()/2);
        Debug.Log("bebi");
    }

    /// <summary>
    /// TODO: Coco e xixi em lugares aleatórios
    /// </summary>
    public IEnumerator PeeOnLocation()
    {
        pee = Resources.Load("Prefabs/Items/Pee") as GameObject;
        Vector3 position = new Vector3(transform.position.x, transform.position.y - GetComponent<Renderer>().bounds.size.y / 2, transform.position.z-5); //Eixo Z tem que ser menor para ficar mais perto da câmera e ativar o OnMouseDown()
        Instantiate(pee, position, Quaternion.identity);
        SaveManager.instance.player.health.PutInPee(-0.5f); //Esvazia pela metade a vontade do animal de fazer xixi
        SaveManager.instance.player.peeLocation.Add(SceneManager.GetActiveScene().name, position);
        yield return new WaitForSeconds(0);
    }

    /// <summary>
    /// Chamado quando o animal evacua.
    /// </summary>
    public IEnumerator PoopOnLocation()
    {
        poop = Resources.Load("Prefabs/Items/Poop") as GameObject;
        Vector3 position = new Vector3(transform.position.x, transform.position.y - GetComponent<Renderer>().bounds.size.y/2, transform.position.z-5);
        Instantiate(poop, position, Quaternion.identity);
        SaveManager.instance.player.health.PutInPoop(-0.5f); //Esvazia pela metade a vontade do animal de fazer cocô
        SaveManager.instance.player.poopLocation.Add(SceneManager.GetActiveScene().name, position);
        yield return new WaitForSeconds(0);
    }

    //public void PoopRandomPlace()
    //{
    //    string sceneName = ReturnSceneName();
    //    Vector3 position = randomPosition(sceneName);
    //    SaveManager.instance.player.poopLocation.Add(sceneName, );
    //}

    //private Vector3 randomPosition(string scene)
    //{
    //    Vector3 position = new Vector3();
    //    int upperBound;
    //    int lowerBound;



    //    position.x =
    //}

    public string ReturnSceneName()
    {
        int random = (int)Random.Range(1, 2);

        switch (random)
        {
            case 1:
                return "MainRoom";
            case 2:
                return "Yard";
            default:
                return "";
        }
    }

    public void PeeRandomPlace()
    {

    }

    public void Play()
    {

    }
}
