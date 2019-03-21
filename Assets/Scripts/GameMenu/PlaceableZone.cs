using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableZone : MonoBehaviour
{
    static string currentZone = "null";
    string zoneName;

    
    private void Start()
    {
        zoneName = this.name;
    }

    void OnMouseOver()
    {
        currentZone = zoneName;
    }

    public static string GetCurrentZone()
    {
        return currentZone;
    }
}
