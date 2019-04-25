using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggerText : MonoBehaviour
{
    Text debuggerText;
    int currentPeriod;
    GameObject currentPet;
    ElementLocation foodPotLocation;
    ElementLocation waterPotLocation;
    ElementLocation petLocation;
    string currentScene;
    bool food;

    private void Awake()
    {
        debuggerText = GetComponent<Text>();
    }

    private void UpdateData()
    {
        GameManager gameManager = GameManager.instance;
        SaveManager saveManager = SaveManager.instance;
        food = saveManager.player.health.GetCleanFoodPot();
        currentPeriod = saveManager.player.timeHelper.currentPeriod;
        foodPotLocation = saveManager.player.foodPotLocation;
        waterPotLocation = saveManager.player.waterPotLocation;
        currentPet = GameObject.FindGameObjectWithTag("PetFather");
        petLocation = currentPet.GetComponentInChildren<Pet>().petCurrentLocation; 
        
        //petLocation = currentPet.transform.GetChild(0).gameObject.GetComponent<Pet>().petCurrentLocation;
    }

    private string MakeDebugString()
    {
        string text = ("Hungry Status: " + food + " PetPosition:" + petLocation.ToString() + " foodPotLocation" + foodPotLocation.ToString() + " waterPotLocation" + waterPotLocation.ToString());
        return text;
    }

    private void Update()
    {
        UpdateData();
        debuggerText.text = MakeDebugString();
    }
}
