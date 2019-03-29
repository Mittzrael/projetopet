using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeyMove : MonoBehaviour
{
    Vector3 camPosX;
    int speed = 2000;

    void Update()
    {
        camPosX = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += camPosX * speed * Time.deltaTime;
    }
}