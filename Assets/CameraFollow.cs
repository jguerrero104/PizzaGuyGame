using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerCar;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float zoomedOutSize = 10f; // Size of the camera's view when zoomed out

    private Camera cam;
    private float originalSize;

    void Start()
    {
        cam = GetComponent<Camera>();
        originalSize = cam.orthographicSize;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = playerCar.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, offset.z);
    }

    public void ToggleZoom()
    {
        cam.orthographicSize = (cam.orthographicSize == originalSize) ? zoomedOutSize : originalSize;
    }
}
