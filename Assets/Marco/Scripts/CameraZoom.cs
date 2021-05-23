using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 4f;


    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Camera.main.fieldOfView -= zoomSpeed;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            Camera.main.fieldOfView += zoomSpeed;
        }
    }
}
