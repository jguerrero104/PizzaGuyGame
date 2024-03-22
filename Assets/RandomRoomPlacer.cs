using UnityEngine;

public class RandomRoomPlacer : MonoBehaviour
{
    public GameObject roomPrefab;
    public int gridWidth = 5;
    public int gridHeight = 5;
    public float roomSize = 10f;

    private Camera mainCamera;
    private bool[,] grid;

    void Start()
    {
        mainCamera = Camera.main;
        grid = new bool[gridWidth, gridHeight];
        PlaceRooms();
    }

    void PlaceRooms()
    {
        // Calculate camera bounds
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector3 cameraPosition = mainCamera.transform.position;

        // Calculate the grid start position based on the camera position and grid size
        Vector3 gridStartPosition = new Vector3(
            cameraPosition.x - (gridWidth * roomSize) / 2 + roomSize / 2,
            cameraPosition.y - (gridHeight * roomSize) / 2 + roomSize / 2,
            0
        );

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (Random.value > 0.5f)
                {
                    Vector3 roomPosition = new Vector3(
                        gridStartPosition.x + x * roomSize,
                        gridStartPosition.y + y * roomSize,
                        0
                    );
                    Instantiate(roomPrefab, roomPosition, Quaternion.identity);
                    grid[x, y] = true;
                }
            }
        }
    }
}
