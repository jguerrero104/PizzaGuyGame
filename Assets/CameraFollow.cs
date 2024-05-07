using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform playerCar; // This will be assigned dynamically
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float zoomedOutSize = 10f;
    
    private Camera cam;
    private float originalSize;

    void Start()
    {
        cam = GetComponent<Camera>();  // Ensure this script is attached to a GameObject with a Camera component
        originalSize = cam.orthographicSize;  // Save the original size to toggle between zoom levels
    }

    void LateUpdate()
    {
        if (playerCar != null)
        {
            Vector3 desiredPosition = playerCar.position + offset;  // Calculate where the camera needs to be based on the player's position and the offset
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);  // Smoothly move the camera towards the desired position
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, offset.z);  // Apply the smoothed position to the camera's transform
        }
    }

    public void SetTarget(Transform newTarget)
    {
        playerCar = newTarget;  // Method to dynamically change the target of the camera
    }

    public void ToggleZoom()
    {
        cam.orthographicSize = (cam.orthographicSize == originalSize) ? zoomedOutSize : originalSize;  // Toggle the zoom level
    }
}
