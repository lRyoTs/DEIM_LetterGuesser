using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public static TimerUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than 1 instance of LifeUI");
        }
        Instance = this;
    }

    public void UpdateTimerText(int timer)
    {
        timerText.text = $"{timer}";
    }
}
