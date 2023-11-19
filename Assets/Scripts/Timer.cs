using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Timer
{
    private const int TIME_TO_GUESS = 15;
    private static int timer = TIME_TO_GUESS;

    public static void ResetTimer() {
        timer = TIME_TO_GUESS;
        TimerUI.Instance.UpdateTimerText(timer);
    }
    public static IEnumerator StartTimerCountdown()
    {
        while (true) {
            yield return new WaitForSeconds(1f);
            //Update timer
            timer--;
            TimerUI.Instance.UpdateTimerText(timer);
        }
    }

    public static bool TimeIsOver() {
        if (timer < 0) {
            return true;
        }
        return false;
    }
}
