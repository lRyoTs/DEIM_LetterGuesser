using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    public string DEFAULT_MESSAGE = "No hints for the moment";
    public string INPUT_MESSAGE = "Select letter from KEYBOARD";

    [SerializeField] private GameObject missText;
    [SerializeField] private TextMeshProUGUI selectedLetterText;
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Button retryButton;


    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than 1 instance of GameUI");
        }    
        Instance = this;
        HideFinishPanel();
        HideMissText();
        ResetSelectedLetterText();

        retryButton.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
    }

    private void Start()
    {
        UpdateHintText(DEFAULT_MESSAGE);
    }

    public void UpdateSelectedLetterText(char letter)
    {
        selectedLetterText.text = $"Your Guess\n{letter}";
        Invoke("ResetSelectedLetterText", 7f);
    }

    public void ResetSelectedLetterText() {
        selectedLetterText.text = INPUT_MESSAGE;
    }

    public void UpdateHintText(string message) {
        hintText.text = message ;
    }

    private void HideFinishPanel() {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        finishPanel.SetActive(false);
    }

    /// <summary>
    /// Function that show if the player won or not
    /// </summary>
    /// <param name="isWin"> boolean to choose which panel to show </param>
    public void ShowFinishPanel(bool isWin) {
        finishPanel.SetActive(true);
        if (isWin) {
            winPanel.SetActive (true);
        } else { 
            losePanel.SetActive (true);
        }
    }

    public void ShowMissText() {
        missText.SetActive(true);
        Invoke("HideMissText", 2.5f);
    }

    public void HideMissText() {
        missText.SetActive(false);
    }
}
