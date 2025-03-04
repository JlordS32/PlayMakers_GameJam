using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private int maxSpawnCount;
    [SerializeField] private Vector2 spawnArea = new(10f, 10f);

    public void Spawn()
    {
        if (prefabs.Count == 0) return;

        for (int i = 0; i < maxSpawnCount; i++)
        {
            Vector3 randomPosition = new Vector3(
                    Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                    0f,
                    Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
                ) + transform.position;

            int randomIndex = Random.Range(0, prefabs.Count);

            Instantiate(prefabs[randomIndex], randomPosition, Quaternion.identity);

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea.x, 0.1f, spawnArea.y));
    }
}
