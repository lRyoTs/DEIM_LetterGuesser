using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Life
{
    private const int INITIAL_LIVES = 3;
    private static int playerLife = INITIAL_LIVES;


    public static void InitializedStaticLife()
    {
        playerLife = INITIAL_LIVES;
        LifeUI.Instance.UpdatePlayerLifeText(playerLife);
    }

    public static int GetPlayerLife() {
        return playerLife;
    }

    public static void ReduceLife() {
        playerLife--;
        LifeUI.Instance.UpdatePlayerLifeText(playerLife);
    }

    public static bool IsAlive() {
        if (playerLife > 0) {
            return true;
        }
        return false;
    }
}
