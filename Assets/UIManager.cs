using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI orderText;

    public void UpdateOrderText(string address, float cost)
    {
        orderText.text = $"Deliver to: {address}\nOrder Cost: ${cost}";
    }
}