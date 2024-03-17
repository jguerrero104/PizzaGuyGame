using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGeneration : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public int CityWidth = 10;
    public int CityHeight = 10;
    public float buildingSpacing = 10f;

    private void Start()
    {
        GenerateCity();
    }

    void GenerateCity()
    {
        for (int x = 0; x < CityWidth; x++)
        {
            for (int y = 0; y < CityHeight; y++)
            {
            GameObject buildingPrefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
            // Calculate the position for the building
            Vector3 position = new Vector3(x * buildingSpacing, y * buildingSpacing, 0);
            // Instantiate the building at the calculated position
            Instantiate(buildingPrefab, position, Quaternion.identity);
            }
        }

        AssignDeliveryAddress();

    }

    void AssignDeliveryAddress()
    {
        int randomX = Random.Range(0, CityWidth);
        int randomY = Random.Range(0, CityHeight);

        Vector3 deliveryAddress = new Vector3(randomX * buildingSpacing, 0, randomY * buildingSpacing);
    }
}