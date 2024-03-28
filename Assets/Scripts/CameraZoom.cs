using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 10f;  // The rate of change of the camera zoom
    public float minZoom = 3f;     // Minimum zoom level
    public float maxZoom = 20f;    // Maximum zoom level

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>(); // Make sure this script is attached to the camera you want to zoom
    }

    void Update()
    {
        // Get the scroll wheel input and invert the scroll direction
        float scroll = -Input.GetAxis("Mouse ScrollWheel");

        // Zoom the camera in or out
        cam.orthographicSize += scroll * zoomSpeed;

        // Clamp the orthographic size to ensure it's within min/max bounds
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
    }
}