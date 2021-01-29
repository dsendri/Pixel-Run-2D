using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CountGame 
{
   private static int gameCount;

    public static int GameCount 
    {
        get 
        {
            return gameCount;
        }
        set 
        {
            gameCount = value;
        }
    }

    public static void Increment() {
        gameCount = gameCount + 1;
    }
}


