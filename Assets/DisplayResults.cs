using UnityEngine;
using TMPro; 

public class DisplayResults : MonoBehaviour
{
    public TextMeshProUGUI timeText; 
    public TextMeshProUGUI earningsText; 

    void Start()
    {
        // Retrieve the delivery time and earnings
        float deliveryTime = PlayerPrefs.GetFloat("DeliveryTime");
        float earnings = PlayerPrefs.GetFloat("Earnings");

        
        timeText.text = "Time: " + deliveryTime.ToString("F2") + " seconds";
        earningsText.text = "Earnings: $" + earnings.ToString("F2");
    }
}
