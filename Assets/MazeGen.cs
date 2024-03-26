using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject deliveryAddressPrefab;
    public GameObject playerCharacterPrefab;
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;

    private bool[,] mazeGrid;

void Start()
{
    // Ensure that the width and height are odd numbers
    width = (width % 2 == 0) ? width + 1 : width;
    height = (height % 2 == 0) ? height + 1 : height;

    mazeGrid = new bool[width, height];
    GenerateMaze(1, 1); // Start generating the maze from a cell that is not on the edge
    InstantiateMaze();
    PlaceDeliveryAddress();
    SpawnPlayerCharacter();
}


void GenerateMaze(int x, int y)
{
    mazeGrid[x, y] = true; // Mark the cell as part of the path

    // Randomly order the directions to move
    int[] directions = { 0, 1, 2, 3 };
    ShuffleArray(directions);

    // Explore each direction
    for (int i = 0; i < directions.Length; i++)
    {
        int dx = 0, dy = 0;
        switch (directions[i])
        {
            case 0: dx = 1; break; // Right
            case 1: dx = -1; break; // Left
            case 2: dy = 1; break; // Up
            case 3: dy = -1; break; // Down
        }

        int newX = x + dx * 2;
        int newY = y + dy * 2;

        // Check if the new cell is within the grid and not already part of the path
        if (newX >= 0 && newX < width && newY >= 0 && newY < height && !mazeGrid[newX, newY])
        {
            // Mark the cells between the current cell and the new cell as part of the path (creates wider paths)
            mazeGrid[x + dx, y + dy] = true;
            mazeGrid[newX, newY] = true;

            // Recursively generate the maze from the new cell
            GenerateMaze(newX, newY);
        }
    }
}


void InstantiateMaze()
{
    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            Vector3 position = new Vector3(x * cellSize, y * cellSize, 0);
            GameObject prefab = mazeGrid[x, y] ? floorPrefab : wallPrefab; // Allow floor prefabs on the edges if they are part of the path
            Instantiate(prefab, position, Quaternion.identity);;
        }
    }
}

 void PlaceDeliveryAddress()
    {
        List<Vector2Int> deadEnds = FindDeadEnds();
        if (deadEnds.Count > 0)
        {
            Vector2Int deliveryAddressPosition = deadEnds[Random.Range(0, deadEnds.Count)];
            Vector3 position = new Vector3(deliveryAddressPosition.x * cellSize, deliveryAddressPosition.y * cellSize, 0);
            Instantiate(deliveryAddressPrefab, position, Quaternion.identity);
        }
    }

    List<Vector2Int> FindDeadEnds()
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                if (mazeGrid[x, y]) // Check if the cell is part of the path
                {
                    int adjacentFloors = 0;
                    if (mazeGrid[x + 1, y]) adjacentFloors++;
                    if (mazeGrid[x - 1, y]) adjacentFloors++;
                    if (mazeGrid[x, y + 1]) adjacentFloors++;
                    if (mazeGrid[x, y - 1]) adjacentFloors++;

                    if (adjacentFloors == 1) // If the cell has only one adjacent floor cell, it's a dead-end
                    {
                        deadEnds.Add(new Vector2Int(x, y));
                    }
                }
            }
        }
        return deadEnds;
    }



void SpawnPlayerCharacter()
{
    List<Vector2Int> floorCells = FindFloorCells();
    if (floorCells.Count > 0)
    {
        Vector2Int spawnPosition = floorCells[Random.Range(0, floorCells.Count)];
        Vector3 position = new Vector3(spawnPosition.x * cellSize, spawnPosition.y * cellSize, 0);
        GameObject playerCharacter = Instantiate(playerCharacterPrefab, position, Quaternion.identity);

        CameraFollow2 cameraFollow2 = Camera.main.GetComponent<CameraFollow2>();
        if (cameraFollow2 != null)
        {
            cameraFollow2.target = playerCharacter.transform;
        }
    }
}


    List<Vector2Int> FindFloorCells()
    {
        List<Vector2Int> floorCells = new List<Vector2Int>();
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                if (mazeGrid[x, y]) // Check if the cell is part of the path
                {
                    floorCells.Add(new Vector2Int(x, y));
                }
            }
        }
        return floorCells;
    }


    void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
