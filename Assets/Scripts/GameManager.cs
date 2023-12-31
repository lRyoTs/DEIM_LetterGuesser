using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const int NUMBER_OF_HINTS = 2;

    private Letter letterToGuess;
    [SerializeField] private char selectedLetter;
    
    private bool busyInput = false;

    private bool isFinished = false;
    private int hintsToCall = NUMBER_OF_HINTS;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than 1 instance of Game Manager");
        }
        Instance = this;
    }
    void Start()
    {
        selectedLetter = Letter.NULL_CHAR;
        Life.InitializedStaticLife();
        hintsToCall = NUMBER_OF_HINTS;

        letterToGuess = new Letter(hintsToCall);
        Timer.ResetTimer();
        StartCoroutine(GuessLetter());
        busyInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinished) {
            if (Input.anyKeyDown && !busyInput) {
                foreach (char c in Input.inputString) {
                    //Check if any input is in the alphabet
                    if (Letter.IsInAlphabet(c)) {
                        selectedLetter = char.ToLowerInvariant(c);
                        GameUI.Instance.UpdateSelectedLetterText(selectedLetter);
                        busyInput = true;
                    }
                }
                
            }
        }
    }

    /// <summary>
    /// Coroutine that wait till there is a selected letter or timer reaches to 0
    /// add check if the answer is correct
    /// </summary>
    /// <returns></returns>
    private IEnumerator GuessLetter() {

        StartCoroutine(Timer.StartTimerCountdown());
        
        do {

            yield return new WaitWhile(() => selectedLetter == Letter.NULL_CHAR && !Timer.TimeIsOver());
            //If the letter selected is correct then Win
            if (letterToGuess.IsSameLetter(selectedLetter))
            {
                Win();
            }
            else {
                //Update Lives left
                GameUI.Instance.ShowMissText();
                Life.ReduceLife();
                if (Life.IsAlive()) //Still has lives then retry
                {
                    //Every miss give hint
                    ShowHint(--hintsToCall);
                    Timer.ResetTimer();                    
                    selectedLetter = Letter.NULL_CHAR;
                }
                else {//Else Game Over
                    Lose();
                }
            }
            busyInput = false;

        } while (!isFinished);
    }

    private void Win() {
        StopAllCoroutines();
        isFinished = true;
        GameUI.Instance.ShowFinishPanel(true);
    }

    private void Lose() {
        StopAllCoroutines();
        isFinished = true;
        GameUI.Instance.ShowFinishPanel(false);
    }

    private void ShowHint(int index) {
        GameUI.Instance.UpdateHintText(letterToGuess.GetHint(index));
    }
}
