using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Pet: MonoBehaviour
{
    [SerializeField]
    private string name;
    private Vector3 position;
    public string screen;
    private GameObject poop;
    private GameObject pee;

    public void Walk()
    {

    }

    public void Eat(Food food)
    {
        //health.PutInHungry(food.GetNutrionalValor());
        Debug.Log("comi");
        SaveManager.instance.player.health.PutInPoop(food.GetNutrionalValor()/2);
    }

    public void Drink(Food food)
    {
        SaveManager.instance.player.health.PutInThirsty(food.GetNutrionalValor());
        SaveManager.instance.player.health.PutInPee(food.GetNutrionalValor()/2);
        Debug.Log("bebi");
    }

    public void Pee()
    {
        pee = Resources.Load("Prefabs/Items/Pee") as GameObject;
        Vector3 position = new Vector3(transform.position.x, transform.position.y - GetComponent<Renderer>().bounds.size.y / 2, transform.position.z-5); //Eixo Z tem que ser menor para ficar mais perto da câmera e ativar o OnMouseDown()
        Instantiate(pee, position, Quaternion.identity);
        SaveManager.instance.player.health.PutInPee(-0.5f); //Esvazia pela metade a vontade do animal de fazer xixi
        SaveManager.instance.player.peeLocation.Add(SceneManager.GetActiveScene().name, position);
    }
    /// <summary>
    /// Chamado quando o animal evacua.
    /// </summary>
    public void Poop()
    {
        poop = Resources.Load("Prefabs/Items/Poop") as GameObject;
        Vector3 position = new Vector3(transform.position.x, transform.position.y - GetComponent<Renderer>().bounds.size.y/2, transform.position.z-5);
        Instantiate(poop, position, Quaternion.identity);
        SaveManager.instance.player.health.PutInPoop(-0.5f); //Esvazia pela metade a vontade do animal de fazer cocô
        SaveManager.instance.player.poopLocation.Add(SceneManager.GetActiveScene().name, position);
    }

    public void Play()
    {

    }
}
