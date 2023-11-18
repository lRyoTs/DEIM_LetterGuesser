using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Letter
{
    public const char NULL_CHAR = '\0';
    private const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";

    private char letter;
    private string[] hints;

    public Letter() {
        hints = new string[2];
        letter = GetRandomLetter();
    }

    private char GetRandomLetter()
    {
        char[] letters = ALPHABET.ToCharArray();
        int index = Random.Range(0, letters.Length);

        hints[0] = index > letters.Length / 2 ? "HINT: The letter is in the SECOND half of the alphabet" 
            : "HINT: The letter is in the FIRST half of the alphabet ";

        hints[1] = IsVowel(letters[index]) ? "HINT: The letter is a vowel" : "HINT: The letter is a consonant";

        return letters[index];
    }

    public char GetLetter() {
        return letter;
    }

    public static bool IsVowel(char letter) {
        char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
        if (vowels.Contains(letter)) {
            return true;
        }
        return false;
    }

    public bool IsSameLetter(char letterSelected) {
        if (letter == letterSelected) {
            return true;
        }
        return false;
    }

    public static bool IsInAlphabet(char letter)
    {
        char[] letters = ALPHABET.ToCharArray();
        if (letters.Contains(letter)) {
            return true;
        }
        return false;
    }
}
