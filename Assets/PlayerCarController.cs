using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public KeyCode zoomOutKey = KeyCode.Space;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 newPosition = rb.position + (Vector2)transform.up * moveVertical * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        float newRotation = rb.rotation - moveHorizontal * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(newRotation);

        if (Input.GetKeyDown(zoomOutKey))
        {
            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.ToggleZoom();
            }
        }
    }
    
}
