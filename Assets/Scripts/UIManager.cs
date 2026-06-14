using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [Header("Buttons")]
    [SerializeField] private GameObject startWaveButton;

    [Header("Instruction UI")]
    [SerializeField] private GameObject instructionPanel;
    [SerializeField] private TextMeshProUGUI instructionText;

    [Header("Messages")]
    [SerializeField] private string scanningMessage = "Move your phone slowly to scan the floor";
    [SerializeField] private string placingMessage = "Tap on the detected surface to place a Tower";
    [SerializeField] private string playingMessage = "Wave incoming";
    
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

    // Update instruction text
    public void UpdateInstructions(GameState state)
    {
        switch (state)
        {
            case GameState.Scanning:
                ShowInstruction(scanningMessage);
                break;
            case GameState.Placing:
                ShowInstruction(placingMessage);
                break;
            case GameState.Playing:
                ShowInstruction(playingMessage);
                HideStartWaveButton();
                break;
        }
    }

    // Display instruction text
    private void ShowInstruction(string message)
    {
        instructionPanel.SetActive(true);
        instructionText.text = message;
    }

    // Hide instruction text
    public void HideInstructions()
    {
        instructionPanel.SetActive(false);
    }
    
    public void ShowStartWaveButton()
    {
        startWaveButton.SetActive(true);
    }

    public void HideStartWaveButton()
    {
        startWaveButton.SetActive(false);
    }
}
