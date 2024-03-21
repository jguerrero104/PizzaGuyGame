using UnityEngine;

public class DeliveryBuilding : MonoBehaviour
{
    public bool isDeliveryAddress = false; // Flag to indicate if this is the delivery address

    public void MarkAsDeliveryAddress(bool isAddress)
    {
        isDeliveryAddress = isAddress;
        // Update the visual indicator based on whether this is the delivery address
        GetComponent<SpriteRenderer>().color = isAddress ? Color.red : Color.white;
    }

    public void CompleteDelivery()
    {
        if (isDeliveryAddress)
        {
            // Handle successful delivery
            Debug.Log("Delivery successful!");
            // Reset the delivery address flag
            isDeliveryAddress = false;
            // Optionally, trigger any delivery completion logic, such as updating the score
        }
    }
}
