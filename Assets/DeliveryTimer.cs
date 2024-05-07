using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeliveryTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeRemaining = 60f; 

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
       
        }
    }

    public void DeliveryCompleted()
    {
        // Handle successful delivery
        Debug.Log("Delivery Completed!");
        
    }
}
