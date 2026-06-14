using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maxEnemies = 10;

    private ARPlaneManager planeManager;
    private int spawnedCount = 0;
    
    private Vector3 lockedSpawnPoint;
    private Vector3 lockedGoalPoint;
    private bool battlefieldLocked = false;

    private void Awake()
    {
        planeManager = FindAnyObjectByType<ARPlaneManager>();
    }

    public void StartSpawning()
    {
        if (!LockBattlefield()) return;
        StartCoroutine(SpawnRoutine());
    }

    private bool LockBattlefield()
    {
        // Find the largest detected plane to use as battlefield
        ARPlane bestPlane = null;
        float largestArea = 0f;

        foreach (var plane in planeManager.trackables)
        {
            float area = plane.size.x * plane.size.y;
            if (area > largestArea)
            {
                largestArea = area;
                bestPlane = plane;
            }
        }

        if (bestPlane == null) return false;

        // Snapshot the spawn and goal points right now
        Vector3 center = bestPlane.transform.position;
        Vector3 right = bestPlane.transform.right;
        float halfSize = bestPlane.size.x * 0.5f;

        lockedSpawnPoint = center - right * halfSize;
        lockedGoalPoint = center + right * halfSize;

        // Lift slightly above the plane surface
        lockedSpawnPoint.y = center.y + 0.01f;
        lockedGoalPoint.y = center.y + 0.01f;

        battlefieldLocked = true;

        Debug.Log($"Battlefield locked. Spawn: {lockedSpawnPoint}, Goal: {lockedGoalPoint}");
        return true;
    }

    private IEnumerator SpawnRoutine()
    {
        while (spawnedCount < maxEnemies)
        {
            SpawnEnemy();
            spawnedCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (!battlefieldLocked) return;

        GameObject enemy = Instantiate(enemyPrefab, lockedSpawnPoint, Quaternion.identity);
        enemy.GetComponent<EnemyBehaviour>().Initialize(lockedGoalPoint);
    }
}