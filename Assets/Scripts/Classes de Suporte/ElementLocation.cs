using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ElementLocation
{
    public string elementName;
    public string sceneName;
    public Vector3 elementPosition = new Vector3();

    public ElementLocation() { }
    public ElementLocation(string name, string scene, Vector3 pos)
    {
        elementName = name;
        sceneName = scene;
        elementPosition = pos;
    }

    public override string ToString()
    {
        return ("Element name: " + elementName + " sceneName: " + sceneName + " elementPosition: " + elementPosition.ToString());
    }
}