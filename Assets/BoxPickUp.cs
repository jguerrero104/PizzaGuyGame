using UnityEngine;

public class BoxPickup : MonoBehaviour
{
    private bool isPlayerInRange = false;

    private void Update()
    {
        // Check if the player is in range and presses the E key
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUpBox();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCharacter"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCharacter"))
        {
            isPlayerInRange = false;
        }
    }

    private void PickUpBox()
    {
        // Generate a new delivery order when the pizza is picked up
        DeliverySystem.GenerateNewOrder();
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateOrderText(DeliverySystem.CurrentOrder.Address, DeliverySystem.CurrentOrder.Cost);
        }

        // Destroy the box to simulate picking it up
        Destroy(gameObject);
    }

}
