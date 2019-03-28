using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invisible : MonoBehaviour
{
    GameObject pet;
    Animator petAnimator;

    private void Coisa()
    {
        Debug.Log("Opa");
    }

    private void Awake()
    {
        pet = gameObject.transform.GetChild(0).gameObject;
        petAnimator = pet.GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    public void StatusVerify()
    {
        Debug.Log("Teste");
        if (pet.GetComponent<Pet>().screen.Equals(SceneManager.GetActiveScene().name))
        {
            pet.SetActive(true);
            BackToOpaque(pet);
        }

        else
        {
            pet.SetActive(false);
        }
    }

    public IEnumerator PetChangeLocation(string scene)
    {
        pet.GetComponent<Pet>().screen = scene;
        petAnimator.Play("Invisible");
        yield return new WaitForSeconds(0.45f);
        pet.SetActive(false);
    }

    private void BackToOpaque(GameObject pet)
    {
        Color petColor = pet.GetComponent<SpriteRenderer>().color;
        Color color = new Color(petColor.r, petColor.g, petColor.b, 1f);
        pet.GetComponent<SpriteRenderer>().color = color;
    }
}
