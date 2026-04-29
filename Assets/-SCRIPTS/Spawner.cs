using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject Skeletonprefab;
    public int number;
    public float SpawnRadius;
    public bool SpawnOnStart = true;
    public bool ChaserSpawn = true;

    [Header("Repeat Spawning")]
    public bool SpawnOnInterval = false;
    public float spawnInterval = 5f; // seconds between each spawn wave

    void Start()
    {
        if (SpawnOnStart)
            SpawnAll();

        if (SpawnOnInterval)
            StartCoroutine(SpawnLoop());

        this.GetComponent<Collider>().enabled = false;
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnAll();
        }
    }

    void SpawnAll()
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 randoPoint = this.transform.position + UnityEngine.Random.insideUnitSphere * SpawnRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randoPoint, out hit, 10.0f, NavMesh.AllAreas))
            {
                Instantiate(Skeletonprefab, hit.position, Quaternion.identity);
            }
        }
    }

    void Update()
    {
    }
}