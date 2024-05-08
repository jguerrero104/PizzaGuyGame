using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public KeyCode zoomOutKey = KeyCode.Space;

    public float collisionSlowdown = 2f;

    private Rigidbody2D rb;
    private float moveVertical;
    private float moveHorizontal;
    private float originalSpeed;
    private bool isColliding = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = speed;
    }

    void Update()
    {
        // Gather input from the keyboard
        moveVertical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");

        // Handle camera zoom
        if (Input.GetKeyDown(zoomOutKey))
        {
            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.ToggleZoom();
            }
        }
    }
void FixedUpdate()
{
    // Handle movement and rotation
    Vector2 newPosition = rb.position + (Vector2)transform.up * moveVertical * (isColliding ? collisionSlowdown : speed) * Time.fixedDeltaTime;
    rb.MovePosition(newPosition);

    float newRotation = rb.rotation - moveHorizontal * rotationSpeed * Time.fixedDeltaTime;
    rb.MoveRotation(newRotation);
}


void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("AICar"))
    {
        speed = collisionSlowdown; // Slow down the car
        isColliding = true;
    }
}

void OnCollisionExit2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("AICar"))
    {
        speed = originalSpeed; // Restore the original speed
        isColliding = false;
    }
}

}
