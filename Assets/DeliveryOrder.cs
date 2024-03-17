using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeliveryOrder
{

    public string Address {get; private set;}
    public float Cost {get; private set;}

    public DeliveryOrder(string address, float cost){
        Address = address;
        Cost = cost;
    }
}

public static class DeliverySystem
{
    public static DeliveryOrder CurrentOrder { get; private set; }

    private static List<string> possibleAddresses = new List<string>
    {
        "1234 Elm Street",
        "5678 Maple Avenue",
        "91011 Oak Road",
        "121314 Pine Circle",
        "151617 Birch Lane"
    };

    public static void GenerateNewOrder()
    {
        // Generate a random address
        int addressIndex = Random.Range(0, possibleAddresses.Count);
        string address = possibleAddresses[addressIndex];

        // Generate a random cost between $5.00 and $20.00
        float cost = Random.Range(5.00f, 20.00f);

        // Round the cost to two decimal places
        cost = Mathf.Round(cost * 100f) / 100f;

        CurrentOrder = new DeliveryOrder(address, cost);
    }
}

public class BoxPickUp : MonoBehaviour
{
    private bool isPlayerInRange = false;

    private void update()
    {
        if( isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUpBox();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("PlayerCharacter")){
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("PlayerCharacter")){
            isPlayerInRange = false;
        }
    }

    private void PickUpBox()
    {
        DeliverySystem.GenerateNewOrder();
        Debug.Log($"New order: Deliver to {DeliverySystem.CurrentOrder.Address} for ${DeliverySystem.CurrentOrder.Cost}");
        Destroy(gameObject);
    }   


}
