using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";
    public const char NULL_CHAR = '\0';
    private const int TIME_TO_GUESS = 15;
    private const int INITIAL_LIVES = 3;
    private const int NUMBER_OF_HINTS = 2;

    private char[] letters;
    [SerializeField] private char letterToGuess;
    [SerializeField] private char selectedLetter;
    private bool busyInput = false;
    private int playerLife = INITIAL_LIVES;
    [SerializeField] private int timerToGuess = 15;
    private bool isFinished = false;
    private string [] hints;

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
        playerLife = INITIAL_LIVES;
        GameUI.Instance.UpdatePlayerLifeText(playerLife);
        letters = ALPHABET.ToCharArray();
        hints = new string[NUMBER_OF_HINTS];
        letterToGuess = GetRandomLetter();
        timerToGuess = TIME_TO_GUESS;
        GameUI.Instance.UpdateTimerText(timerToGuess);
        StartCoroutine(GuessLetter());
        busyInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinished) {
            if (Input.anyKeyDown && !busyInput) {
                Debug.Log(Input.inputString);
                foreach (char c in Input.inputString) {
                    selectedLetter = char.ToLowerInvariant(c);
                }
                GameUI.Instance.UpdateSelectedLetterText(selectedLetter);
                busyInput = true;
            }
        }
    }

    private char GetRandomLetter() {
        int index = Random.Range(0, letters.Length);

        hints[0] = GetFirstHint(index);
        hints[1] = GetLastHint(letters[index]);

        return letters[index];
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
                    ShowHint(playerLife%NUMBER_OF_HINTS);
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
            busyInput = false;

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
        //FinishPanel(false)
    }

    private string GetFirstHint(int index) {
        string message = "";
        if (index > (letters.Length / 2))
        {
            message = "HINT: The letter is in the SECOND half of the alphabet";
        }
        else {
            message = "HINT: The letter is in the FIRST half of the alphabet ";
        }

        return message;
    }

    private string GetLastHint(char letter)
    {

        char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
        string message = "";

        if (vowels.Contains(letter)) {
            message = "HINT: The letter is a vowel";
        } else {
            message = "HINT: The letter is a consonant";
        }

        return message;
    }

    private void ShowHint(int index) {
        GameUI.Instance.UpdateHintText(hints[index]);
    }
}
