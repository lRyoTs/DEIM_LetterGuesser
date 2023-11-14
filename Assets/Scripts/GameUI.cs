using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI playerLifeText;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than 1 instance of GameUI");
        }    
        Instance = this;
    }

    public void UpdateTimerText(int timer)
    {
        timerText.text = $"{timer}";
    }

    public void UpdatePlayerLifeText(int playerLife)
    {
        playerLifeText.text = $"Tries: {playerLife}";
    }
}
