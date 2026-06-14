using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TowerBehaviour : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 0.5f;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private int damagePerShot = 1;
    [SerializeField] private float shotVisualDuration = 0.1f;

    private float nextFireTime = 0f;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        lineRenderer.material.color = Color.yellow;
    }

    private void Update()
    {
        // Only shoot during Playing state
        if (GameManager.Instance.CurrentState != GameState.Playing) return;

        // Respect the fire rate cooldown
        if (Time.time < nextFireTime) return;

        EnemyBehaviour target = FindClosestEnemy();
        if (target != null)
        {
            Shoot(target);
            nextFireTime = Time.time + fireRate;
        }
    }

    private EnemyBehaviour FindClosestEnemy()
    {
        // Find all enemies in the scene
        EnemyBehaviour[] allEnemies = FindObjectsByType<EnemyBehaviour>(FindObjectsSortMode.None);

        EnemyBehaviour closest = null;
        float closestDistance = detectionRadius;

        foreach (var enemy in allEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }

    private void Shoot(EnemyBehaviour target)
    {
        // Deal damage instantly (hitscan)
        target.TakeDamage(damagePerShot);

        // Flash the line renderer as a visual
        StartCoroutine(ShowShotVisual(target.transform.position));
    }

    private System.Collections.IEnumerator ShowShotVisual(Vector3 targetPosition)
    {
        // Draw line from tower to target
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, targetPosition);
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(shotVisualDuration);

        lineRenderer.enabled = false;
    }
}