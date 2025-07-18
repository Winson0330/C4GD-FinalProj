using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawn : MonoBehaviour
{
    public GameObject[] cloudPrefabs = new GameObject[3]; // Assign 3 prefabs in Inspector
    public float spawnInterval = 2f;
    public float cloudSpeed = 2f;
    public float yMin = -2f, yMax = 2f;
    public float spawnX = 10f;
    public float destroyX = -20f;
    public Vector2 sizeRange = new Vector2(0.5f, 1.5f);

    private float spawnTimer = 0f;
    private List<GameObject> activeClouds = new List<GameObject>();

    void Update()
    {
        // Spawn clouds based on interval
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnCloud();
            spawnTimer = 0f;
        }

        // Move and clean up clouds
        for (int i = activeClouds.Count - 1; i >= 0; i--)
        {
            GameObject cloud = activeClouds[i];
            if (cloud != null)
            {
                cloud.transform.position += Vector3.left * cloudSpeed * Time.deltaTime;

                if (cloud.transform.position.x < destroyX)
                {
                    Destroy(cloud);
                    activeClouds.RemoveAt(i);
                }
            }
        }
    }

    void SpawnCloud()
    {
        int index = Random.Range(0, cloudPrefabs.Length);
        Vector3 spawnPos = new Vector3(spawnX, Random.Range(yMin, yMax), 0f);
        GameObject newCloud = Instantiate(cloudPrefabs[index], spawnPos, Quaternion.identity);

        float randomScale = Random.Range(sizeRange.x, sizeRange.y);
        newCloud.transform.localScale = new Vector3(randomScale, randomScale, 1f);

        activeClouds.Add(newCloud);
    }
}
