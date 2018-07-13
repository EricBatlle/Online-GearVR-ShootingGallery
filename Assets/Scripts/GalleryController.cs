using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GalleryController : NetworkBehaviour {

    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float spawnTime = 2.0f;
    [SerializeField] private Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

    private void Start()
    {
        InvokeRepeating("SpawnTarget",spawnTime,spawnTime);
    }

    [ServerCallback]
    private void SpawnTarget()
    {
        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        NetworkServer.Spawn(Instantiate(targetPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation));
    }
}
