using UnityEngine;

public class PCInteriorBuilding : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public Sprite idleSprite; // Idle sprite to be used when the player is idle

    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite; // To store the original sprite
    private Rigidbody2D rb;
    private bool isGrounded;

void Start()
{
    spriteRenderer = GetComponent<SpriteRenderer>();
    originalSprite = spriteRenderer.sprite; // Store the original sprite
    rb = GetComponent<Rigidbody2D>();
    rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Freeze rotation on the Z-axis
}


void Update()
{
    // Check if the player is grounded
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    // Horizontal movement
    float moveHorizontal = Input.GetAxis("Horizontal");
    rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

    // Jumping
    if (isGrounded && Input.GetKeyDown(KeyCode.Space))
    {
    rb.velocity = new Vector2(rb.velocity.x, 0); // Reset the vertical velocity before applying the jump force
    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    // Flip the sprite based on the direction of movement
    if (moveHorizontal > 0)
    {
        spriteRenderer.flipX = false;
    }
    else if (moveHorizontal < 0)
    {
        spriteRenderer.flipX = true;
    }

    // Change to the idle sprite if the player is not moving
    spriteRenderer.sprite = (moveHorizontal == 0 && isGrounded) ? idleSprite : originalSprite;
}


    void OnDrawGizmos()
    {
        // Draw the ground check radius for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
