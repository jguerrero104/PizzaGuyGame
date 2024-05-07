using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public Transform spawnPoint;
    
    void Start()
    {

        transform.position = spawnPoint.position;    
    }
}


