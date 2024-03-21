using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeliveryTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeRemaining = 60f; // Set this to the desired starting time

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time Remaining: " + Mathf.RoundToInt(timeRemaining);
        }
        else
        {
            // Time's up, handle delivery failure
            Debug.Log("Delivery Failed!");
            // Optionally, reload the scene or transition to a failure screen
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void DeliveryCompleted()
    {
        // Handle successful delivery
        Debug.Log("Delivery Completed!");
        // Optionally, transition to a success screen or the next level
    }
}
