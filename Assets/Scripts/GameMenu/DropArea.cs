using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    static string currentDropArea = "null";
    string dropAreaName;

    private void Start()
    {
        dropAreaName = this.name;
    }

    public void OnDropAreaEnter()
    {
        currentDropArea = dropAreaName;
    }

    public void OnDropAreaExit()
    {
        currentDropArea = "null" ;
    }

    public static string GetCurrentDropArea()
    {
        return currentDropArea;
    }
}
