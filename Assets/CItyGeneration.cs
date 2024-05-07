using UnityEngine;
using System.Collections.Generic;

public class CityGeneration : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public GameObject horizontalRoadPrefab;
    public GameObject verticalRoadPrefab;
    public GameObject carPrefab;
    public int cityWidth = 10;
    public int cityHeight = 10;
    public float buildingSpacing = 10f;
    public float roadWidth = 5f;
    public int numberOfCars = 5;
    private HashSet<Vector2Int> roadTiles;
    private List<GameObject> buildings = new List<GameObject>();
    private Graph cityGraph;

    private void Start()
    {
        DeliveryTimerManager.StartTimer();
        GenerateCity();
    }

        void GenerateCity()
    {
        roadTiles = new HashSet<Vector2Int>();

        for (int x = 0; x < cityWidth; x++)
        {
            for (int y = 0; y < cityHeight; y++)
            {
                Vector3 position = new Vector3(x * (buildingSpacing + roadWidth), y * (buildingSpacing + roadWidth), 0);
                Vector2Int gridPosition = new Vector2Int(x, y);

                if (x % 2 == 0)
                {
                    // Place vertical roads in every other column
                    Instantiate(verticalRoadPrefab, position, Quaternion.identity);
                    roadTiles.Add(gridPosition);
                }
                else
                {
                    if (y % 2 == 0)
                    {
                        // Place horizontal roads in every other row between vertical roads
                        Instantiate(horizontalRoadPrefab, position, Quaternion.identity);
                        roadTiles.Add(gridPosition);
                    }
                    else
                    {
                        // Place buildings in the remaining spaces
                        GameObject buildingPrefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
                        GameObject buildingInstance = Instantiate(buildingPrefab, position, Quaternion.identity);
                        buildings.Add(buildingInstance);
                        // Do not add gridPosition to roadTiles since it's a building
                    }
                }
            }
        }

        // Create the graph with road tiles only
        cityGraph = new Graph(cityWidth, cityHeight, roadTiles);
        AssignDeliveryAddress();
        CreateBoundaryWalls();
        SpawnAICars();
    }


    void CreateBoundaryWalls()
    {
        float halfRoadWidth = roadWidth / 2;
        float offset = 5;
        float cityBoundaryX = cityWidth * (buildingSpacing + roadWidth) - halfRoadWidth;
        float cityBoundaryY = cityHeight * (buildingSpacing + roadWidth) - halfRoadWidth;
        float wallThickness = 3f;
        float wallHeight = 3f;

        Vector3 topWallPos = new Vector3(cityBoundaryX / 2, cityBoundaryY + halfRoadWidth + wallThickness / 2, 0);
        Vector3 bottomWallPos = new Vector3(cityBoundaryX / 2, -halfRoadWidth - wallThickness / 2 - offset, 0);
        Vector3 leftWallPos = new Vector3(-halfRoadWidth - wallThickness / 2 - offset, cityBoundaryY / 2, 0);
        Vector3 rightWallPos = new Vector3(cityBoundaryX + halfRoadWidth + wallThickness / 2, cityBoundaryY / 2, 0);

        Vector3 horizontalWallScale = new Vector3(cityBoundaryX + wallThickness + (2 * offset), wallThickness, wallHeight);
        Vector3 verticalWallScale = new Vector3(wallThickness, cityBoundaryY + wallThickness + offset, wallHeight);

        CreateWall(topWallPos, horizontalWallScale);
        CreateWall(bottomWallPos, horizontalWallScale);
        CreateWall(leftWallPos, verticalWallScale);
        CreateWall(rightWallPos, verticalWallScale);
    }

    void CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = new GameObject("Wall");
        SpriteRenderer renderer = wall.AddComponent<SpriteRenderer>();
        renderer.sprite = null;
        renderer.color = Color.gray;
        wall.transform.position = position;
        wall.transform.localScale = scale;
        BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(scale.x, scale.y);
    }

    void SpawnAICars()
    {
        for (int i = 0; i < numberOfCars; i++)
        {
            Vector2Int start = GetRandomRoadTile();
            Vector2Int end = GetRandomRoadTile();
            GameObject carInstance = Instantiate(carPrefab, new Vector3(start.x * (buildingSpacing + roadWidth), start.y * (buildingSpacing + roadWidth), 0), Quaternion.identity);
            AICar carScript = carInstance.GetComponent<AICar>();
            carScript.currentPos = start;
            carScript.targetPos = end;
            carScript.cityGraph = cityGraph;
        }
    }

     Vector2Int GetRandomRoadTile()
    {
        int index = Random.Range(0, roadTiles.Count);
        foreach (Vector2Int tile in roadTiles)
        {
            if (index == 0)
            {
                return tile;
            }
            index--;
        }
        return new Vector2Int(0, 0); // Fallback in case something goes wrong
    }

    void AssignDeliveryAddress()
    {
        if (buildings.Count > 0)
        {
            foreach (GameObject building in buildings)
            {
                DeliveryBuilding dbComponent = building.GetComponent<DeliveryBuilding>();
                if (dbComponent != null)
                {
                    dbComponent.MarkAsDeliveryAddress(false);
                }
            }

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
