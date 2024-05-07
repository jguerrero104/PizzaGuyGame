using UnityEngine;
using System.Collections.Generic;

public class AICar : MonoBehaviour
{
    public float speed = 5f;
    public Vector2Int currentPos;
    public Vector2Int targetPos;
    public Graph cityGraph;
    private List<Vector2Int> path;
    private int pathIndex = 0;
    private Rigidbody2D rb;
    public Sprite carSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        if (carSprite != null)
        {
            spriteRenderer.sprite = carSprite;
        }
        else
        {
            Debug.LogWarning("Car sprite not assigned.");
        }

        // Get the shortest path
        path = Dijkstra.FindShortestPath(cityGraph, currentPos, targetPos);
    }

    void Update()
    {
        MoveToNextWaypoint();
    }

    void MoveToNextWaypoint()
    {
        if (path == null || pathIndex >= path.Count)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 targetPosition = new Vector2(path[pathIndex].x * 10, path[pathIndex].y * 10);

        if (Vector2.Distance(rb.position, targetPosition) > 0.1f)
        {
            Vector2 direction = (targetPosition - rb.position).normalized;
            UpdateSpriteDirection(direction);

            // Only move horizontally or vertically
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                direction = new Vector2(Mathf.Sign(direction.x), 0);
            }
            else
            {
                direction = new Vector2(0, Mathf.Sign(direction.y));
            }

            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
            // Align the car precisely to the target position
            rb.position = targetPosition;
            pathIndex++;
        }
    }

    void UpdateSpriteDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            // Facing right
            spriteRenderer.flipX = false;
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.x < 0)
        {
            // Facing left
            spriteRenderer.flipX = true;
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.y > 0)
        {
            // Facing up
            spriteRenderer.flipX = false;
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (direction.y < 0)
        {
            // Facing down
            spriteRenderer.flipX = false;
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }
}
