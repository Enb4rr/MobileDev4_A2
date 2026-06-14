using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f;
    [SerializeField] private int health = 3;

    private Vector3 goal;
    private bool isInitialized = false;

    // Called by the spawner to give this enemy its destination
    public void Initialize(Vector3 goalPosition)
    {
        goal = goalPosition;
        isInitialized = true;

        // Face the goal immediately on spawn
        transform.LookAt(new Vector3(goal.x, transform.position.y, goal.z));
    }

    private void Update()
    {
        if (!isInitialized) return;

        // Move toward the goal every frame
        transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);

        // Reached the goal
        if (Vector3.Distance(transform.position, goal) < 0.05f)
        {
            OnReachedGoal();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnReachedGoal()
    {
        Debug.Log("Enemy reached the goal!");
        Destroy(gameObject);
    }
}