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
    private string hint1;
    private string hint2;

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
                GameUI.Instance.UpdateSelectedLetterText(selectedLetter);
            }
        }
    }

    private char GetRandomLetter() {
        int index = Random.Range(0, letters.Length);
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
            //If the letter selected is correct then Win
            if (selectedLetter == letterToGuess)
            {
                CancelInvoke();
                isFinished = true;
                GameUI.Instance.ShowWinPanel();
                Debug.Log("You won");
            }
            else {
                //Update Lives left
                playerLife--;
                GameUI.Instance.UpdatePlayerLifeText(playerLife);
                if (playerLife > 0) //Still has lives then retry
                {
                    //Every miss give hint
                    timerToGuess = TIME_TO_GUESS;
                    GameUI.Instance.UpdateTimerText(timerToGuess);
                    selectedLetter = NULL_CHAR;
                    Debug.Log("Try again");
                }
                else {//Else Game Over
                    CancelInvoke();
                    isFinished = true;
                    GameUI.Instance.ShowLosePanel();
                    Debug.Log("You lost");
                }
            }

        } while (!isFinished);
    }

    private void TimerCountdown()
    {
        //Update timer
        timerToGuess--;
        GameUI.Instance.UpdateTimerText(timerToGuess);
    }

    private void Win() {
        //FinishPanel(true)
    }

    private void Lose() {
        //FInishPanel(false)
    }

    private string GetFirstHint(int index) {
        string message = "";
        if (index > (letters.Length / 2)) {
            message = "Hint: The letter is greater";
        }
        return message;
    }
    
}
