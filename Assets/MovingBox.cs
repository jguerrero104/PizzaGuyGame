using UnityEngine;

public class MovingBox : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float speed = 2f;
    public float waitTime = 1f; // Time to wait at each end point before moving back

    private Vector3 currentTarget;
    private float waitTimer;

    void Start()
    {
        transform.position = startPoint;
        currentTarget = endPoint;
        waitTimer = waitTime;
    }

    void Update()
    {
        // Move the box towards the current target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        // Check if the box has reached the target
        if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
        {
            // Wait at the target for a specified time
            if (waitTimer > 0)
            {
                waitTimer -= Time.deltaTime;
            }
            else
            {
                // Switch the target and reset the timer
                currentTarget = (currentTarget == startPoint) ? endPoint : startPoint;
                waitTimer = waitTime;
            }
        }
    }
}
