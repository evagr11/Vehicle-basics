using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> obstacles;
    [SerializeField] private List<GameObject> collectablePrefabs;

    private int lastObstacleIndex = -1;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        if (obstacles.Count == 0 || collectablePrefabs.Count == 0)
        {
            Debug.LogWarning("CollectableSpawner: faltan obstáculos o prefabs asignados en el Inspector.");
            return;
        }

        int newIndex;
        do
        {
            newIndex = Random.Range(0, obstacles.Count);
        } while (newIndex == lastObstacleIndex && obstacles.Count > 1);

        lastObstacleIndex = newIndex;
        Transform selectedObstacle = obstacles[newIndex];

        int collectableIndex = Random.Range(0, collectablePrefabs.Count);
        GameObject selectedPrefab = collectablePrefabs[collectableIndex];

        Vector3 spawnPosition = selectedObstacle.position;
        spawnPosition.y = 1f;

        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }
}
