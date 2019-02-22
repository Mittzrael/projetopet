using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bondaries
{
    public float max;
    public float min;
}

public class CameraSwipe : MonoBehaviour
{
    public Bondaries xPosition;
    public float speed = 0.1F;
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * speed, 0, 0);
            transform.position = new Vector3(Mathf.Clamp((transform.position.x), xPosition.min, xPosition.max), transform.position.y, transform.position.z);
        }
    }


}

