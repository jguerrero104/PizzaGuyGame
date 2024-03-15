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
    public static DeliveryOrder CurrentOrder{get; private set;}

    public static void GenerateNewOrder(){
        string address = "1234 Elm Street";
        float cost = 5.00f;
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
