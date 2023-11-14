using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    private const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";
    public const char NULL_CHAR = '\0';
    private const int TIME_TO_GUESS = 15;

    private char [] letters;
    [SerializeField] private char letterToGuess;
    [SerializeField] private char selectedLetter;
    private int playerLife = 3;
    [SerializeField] private int timerToGuess = 15;
    private bool isFinished = false;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than 1 instance of Game Manager");
        }
        Instance = this;
    }
    void Start()
    {
        selectedLetter = NULL_CHAR;
        playerLife = 3;
        GameUI.Instance.UpdatePlayerLifeText(playerLife);
        letters = ALPHABET.ToCharArray();
        letterToGuess = GetRandomLetter();
        timerToGuess = TIME_TO_GUESS;
        GameUI.Instance.UpdateTimerText(timerToGuess);
        StartCoroutine(GuessLetter());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinished) {
            if (Input.anyKeyDown) {
                foreach(char c in Input.inputString) {
                    selectedLetter = char.ToLowerInvariant(c);
                }
            }
        }
    }

    private char GetRandomLetter() {
        return letters[Random.Range(0,letters.Length)];
    }

    /// <summary>
    /// Coroutine that wait till there is a selected letter or timer reaches to 0
    /// </summary>
    /// <returns></returns>
    private IEnumerator GuessLetter() {
        InvokeRepeating("TimerCountdown", 1, 1f);
        do {
            yield return new WaitWhile(() => selectedLetter == NULL_CHAR && timerToGuess > 0);
            
            if (selectedLetter == letterToGuess)
            {
                CancelInvoke();
                isFinished = true;
            }
            else {
                playerLife--;
                GameUI.Instance.UpdatePlayerLifeText(playerLife);
                if (playerLife > 0)
                {
                    timerToGuess = TIME_TO_GUESS;
                    GameUI.Instance.UpdateTimerText(timerToGuess);
                    selectedLetter = NULL_CHAR;
                }
                else {
                    CancelInvoke();
                    isFinished = true;
                }
            }

        } while (!isFinished);
    }

    private void TimerCountdown()
    {
        timerToGuess--;
        GameUI.Instance.UpdateTimerText(timerToGuess);
    }
}
