using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetState(GameState.Scanning);
    }

    // Sets a new state in the machine
    public void SetState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log($"Game State: {newState}");
        OnStateChanged(newState);
    }

    // Handle new state
    private void OnStateChanged(GameState state)
    {
        // Update UI whenever state changes
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateInstructions(state);
        
        switch (state)
        {
            case GameState.Scanning:
                break;
            case GameState.Placing:
                break;
            case GameState.Playing:
                FindAnyObjectByType<EnemySpawner>()?.StartSpawning();
                break;
        }
    }
    
    public void StartWave()
    {
        SetState(GameState.Playing);
    }
}
