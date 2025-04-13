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

        GameObject collectibleInstance = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);


    }
}
