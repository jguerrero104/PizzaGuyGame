using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerCar; // Reference to the player car Transform
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public Vector3 offset; // Offset between the camera and the car

    void LateUpdate()
    {
        Vector3 desiredPosition = playerCar.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, offset.z);
    }
}
