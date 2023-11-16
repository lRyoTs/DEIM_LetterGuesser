using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    public static string DEFAULT_MESSAGE = "No hints for the moment";
    
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI playerLifeText;
    [SerializeField] private TextMeshProUGUI selectedLetterText;
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than 1 instance of GameUI");
        }    
        Instance = this;
    }

    private void Start()
    {
        HidePanels();
        UpdateHintText(DEFAULT_MESSAGE);
    }

    public void UpdateTimerText(int timer)
    {
        timerText.text = $"{timer}";
    }

    public void UpdatePlayerLifeText(int playerLife)
    {
        playerLifeText.text = $"Tries: {playerLife}";
    }

    public void UpdateSelectedLetterText(char letter)
    {
        selectedLetterText.text = $"Your Guess\n{letter}";
    }

    public void UpdateHintText(string message) {
        hintText.text = message ;
    }

    public void ShowWinPanel() {
        winPanel.SetActive(true);
    }

    public void ShowLosePanel() {  
        losePanel.SetActive(true);
    }

    private void HidePanels() {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    private void ShowFinishPanel(bool isWin) {
        if (isWin) {
            winPanel.SetActive (true);
        } else { 
            losePanel.SetActive (false);
        }
    }
}
