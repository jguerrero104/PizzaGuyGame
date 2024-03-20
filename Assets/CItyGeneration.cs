using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGeneration : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public GameObject horizontalRoadPrefab;
    public GameObject verticalRoadPrefab;
    public int cityWidth = 10;
    public int cityHeight = 10;
    public float buildingSpacing = 10f;
    public float roadWidth = 5f;

    private void Start()
    {
        GenerateCity();
    }

void GenerateCity()
{
    for (int x = 0; x < cityWidth; x++)
    {
        for (int y = 0; y < cityHeight; y++)
        {
            Vector3 position = new Vector3(x * (buildingSpacing + roadWidth), y * (buildingSpacing + roadWidth), 0);

            if (x % 2 == 0)
            {
                // Place vertical roads in every other column
                Instantiate(verticalRoadPrefab, position, Quaternion.identity);
            }
            else
            {
                if (y % 2 == 0)
                {
                    // Place horizontal roads in every other row between vertical roads
                    Instantiate(horizontalRoadPrefab, position, Quaternion.identity);
                }
                else
                {
                    // Place buildings in the remaining spaces
                    GameObject buildingPrefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
                    Instantiate(buildingPrefab, position, Quaternion.identity);
                }
            }
        }
    }

    // Assign a random building as the delivery address
    AssignDeliveryAddress();
}








    void AssignDeliveryAddress()
    {
        int randomX = Random.Range(0, cityWidth);
        int randomY = Random.Range(0, cityHeight);

        Vector3 deliveryAddress = new Vector3(randomX * buildingSpacing, 0, randomY * buildingSpacing);
    }
}