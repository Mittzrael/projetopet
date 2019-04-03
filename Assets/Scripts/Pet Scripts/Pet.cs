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
        ///Play Animation
        Eat(SaveManager.instance.player.foodPot.content);
        yield return new WaitForSeconds(0);
    }

    public void Eat(Food food)
    {
        SaveManager.instance.player.health.PutInHungry(food.GetNutrionalValor());
        SaveManager.instance.player.health.PutInPoop(food.GetNutrionalValor()/2);
        Debug.Log("comi");
    }

    public IEnumerator Drink()
    {
        ///Play Animation
        Drink(SaveManager.instance.player.waterPot.content);
        yield return new WaitForSeconds(0);
    }

    public void Drink(Food food)
    {
        SaveManager.instance.player.health.PutInThirsty(food.GetNutrionalValor());
        SaveManager.instance.player.health.PutInPee(food.GetNutrionalValor()/2);
        Debug.Log("bebi");
    }

    public void Play()
    {

    }

    public IEnumerator PeeOnLocation()
    {
        pee = Resources.Load("Prefabs/Items/Pee") as GameObject;
        Vector3 position = new Vector3(transform.position.x, transform.position.y - GetComponent<Renderer>().bounds.size.y / 2, transform.position.z-5); //Eixo Z tem que ser menor para ficar mais perto da câmera e ativar o OnMouseDown()
        Instantiate(pee, position, Quaternion.identity);
        SaveManager.instance.player.health.PutInPee(-0.5f); //Esvazia pela metade a vontade do animal de fazer xixi
        SaveManager.instance.player.peeLocation.Add(petCurrentLocation.sceneName, position);
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
        SaveManager.instance.player.poopLocation.Add(petCurrentLocation.sceneName, position);
        yield return new WaitForSeconds(0);
    }

    public void PoopRandomPlace()
    {
        string sceneName = ReturnSceneName();
        Vector3 position = RandomPosition(sceneName); 
        SaveManager.instance.player.poopLocation.Add(sceneName, position);
    }

    public void PeeRandomPlace()
    {
        string sceneName = ReturnSceneName();
        Vector3 position = RandomPosition(sceneName);
        SaveManager.instance.player.peeLocation.Add(sceneName, position);
    }

    private Vector3 RandomPosition(string scene)
    {
        Vector3 position = new Vector3();

        if (scene.Equals("MainRoom"))
        {
            position.x = Random.Range(-2700, 2700);
            position.y = Random.Range(-434, -607);
            position.z = -5;
        }

        else if (scene.Equals("Yard"))
        {
            position.x = Random.Range(-1718, 1700);
            position.y = Random.Range(-566, 607);
            position.z = -5;
        }

        else
        {
            Debug.LogError("Não encontrou scene com o nome solicitado");
        }
        
        return position;
    }

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
                Debug.LogError("Não encontrou scene com o nome solicitado");
                return "";
        }
    }
}
