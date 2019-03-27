using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pee : Waste
{
    public override void Save()
    {
        SaveManager.instance.player.peeLocation.Remove(SceneManager.GetActiveScene().name, gameObject.transform.position);
    }
}