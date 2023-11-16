using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI playerLifeText;
    [SerializeField] private TextMeshProUGUI selectedLetterText;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;

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

    public void ShowWinPanel() {
        WinPanel.SetActive(true);
    }

    public void ShowLosePanel() {  
        LosePanel.SetActive(true);
    }

    private void HidePanels() {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }

}
