using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerInCity : MonoBehaviour
{
    public GameObject playerPrefab;

    [SerializeField] private int cityWidth = 10;
    [SerializeField] private int cityHeight = 10;
    [SerializeField] private float buildingSpacing = 10f;
    [SerializeField] private float roadWidth = 5f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

void SpawnPlayer()
{
    float centerX = (cityWidth * (buildingSpacing + roadWidth)) / 2;
    float centerY = (cityHeight * (buildingSpacing + roadWidth)) / 2;

    Vector3 spawnPosition = new Vector3(centerX, centerY, 0);
    GameObject player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

    // Find the CameraFollow component and set the new player as the target
    CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
    if (cameraFollow != null)
    {
        cameraFollow.SetTarget(player.transform);
    }
}



    // Update is called once per frame
    void Update()
    {
        
    }
}
