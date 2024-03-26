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

     private List<GameObject> buildings = new List<GameObject>();

    private void Start()
    {
        DeliveryTimerManager.StartTimer();
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

                        // Place buildings in the remaining spaces and add them to the list
                        GameObject buildingPrefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
                        GameObject buildingInstance = Instantiate(buildingPrefab, position, Quaternion.identity);
                        buildings.Add(buildingInstance); // Add the building to the list
                    }
                }
            }
        }

        // Assign a random building as the delivery address
        AssignDeliveryAddress();
    }





    void AssignDeliveryAddress()
    {
        if (buildings.Count > 0)
        {
            // Reset previous delivery addresses
            foreach (GameObject building in buildings)
            {
                DeliveryBuilding dbComponent = building.GetComponent<DeliveryBuilding>();
                if (dbComponent != null)
                {
                    dbComponent.MarkAsDeliveryAddress(false);
                }
            }

            // Assign a new random building as the delivery address
            int randomIndex = Random.Range(0, buildings.Count);
            GameObject deliveryBuilding = buildings[randomIndex];
            DeliveryBuilding dbComponentForDelivery = deliveryBuilding.GetComponent<DeliveryBuilding>();
            if (dbComponentForDelivery != null)
            {
                dbComponentForDelivery.MarkAsDeliveryAddress(true);
            }
        }
    }


}