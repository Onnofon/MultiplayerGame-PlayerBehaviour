using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ResourceSpawner : NetworkBehaviour
{
    public GameObject resourcePrefab;
    public int numberOfResources;

    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfResources; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 0.0f, Random.Range(-8.0f, 8.0f));
            Quaternion spawnRotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 180.0f), 0);

            GameObject resource = (GameObject)Instantiate(resourcePrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(resource);
        }
    }

}
