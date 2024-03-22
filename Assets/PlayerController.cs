using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public Sprite idleSprite; // Idle sprite to be used when the player is idle for 3 seconds

    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite; // To store the original sprite
    private float idleTimer = 0f; // Timer to track idle time

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite; // Store the original sprite
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Create movement vector based only on horizontal input
        Vector2 movement = new Vector2(moveHorizontal, 0);

        // Move the player
        transform.position = (Vector2)transform.position + movement * speed * Time.deltaTime;

        // Flip the sprite based on the direction of movement
        if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false;
            idleTimer = 0f; // Reset the idle timer
            spriteRenderer.sprite = originalSprite; // Reset to original sprite
        }
        else if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true;
            idleTimer = 0f; // Reset the idle timer
            spriteRenderer.sprite = originalSprite; // Reset to original sprite
        }
        else
        {
            // Increment the idle timer if the player is not moving
            idleTimer += Time.deltaTime;
        }

        // Change to the idle sprite if the player has been idle for more than 3 seconds
        if (idleTimer > 3f)
        {
            spriteRenderer.sprite = idleSprite;
        }
    }
}
