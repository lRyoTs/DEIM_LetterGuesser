using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeUI : MonoBehaviour
{
    public static LifeUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI playerLifeText;
    
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than 1 instance of LifeUI");
        }
        Instance = this;
    }

    public void UpdatePlayerLifeText(int playerLife)
    {
        playerLifeText.text = $"Tries: {playerLife}";
    }
}
