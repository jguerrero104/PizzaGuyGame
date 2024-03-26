using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteDelivery : MonoBehaviour
{
    private bool playerOnDeliveryPoint = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCharacter"))
        {
            playerOnDeliveryPoint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCharacter"))
        {
            playerOnDeliveryPoint = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerOnDeliveryPoint && Input.GetKeyDown(KeyCode.E))
        {
            // Stop the delivery timer
            DeliveryTimerManager.StopTimer();

            // Calculate earnings (example calculation, you can adjust this)
            float deliveryTime = DeliveryTimerManager.GetDeliveryTime();
            float earnings = Mathf.Max(100 - deliveryTime, 0); // Example: 100 minus the time taken, minimum of 0

            // Save the delivery time and earnings for access in the next scene
            PlayerPrefs.SetFloat("DeliveryTime", deliveryTime);
            PlayerPrefs.SetFloat("Earnings", earnings);

            // Load the results scene
            SceneManager.LoadScene("ResultsScene");
        }
    }
}
