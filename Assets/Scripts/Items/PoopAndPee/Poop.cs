using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Poop : Waste
{ 
    public override void Save()
    {
        SaveManager.instance.player.poopLocation.Remove(SceneManager.GetActiveScene().name, gameObject.transform.position);
    }
}
