using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pref
{
    public static int Coin {
        set => PlayerPrefs.SetInt(PrefKey.Coin.ToString(), value);
        get => PlayerPrefs.GetInt(PrefKey.Coin.ToString(), 0);
    }

    public static int Level {
        set => PlayerPrefs.SetInt(PrefKey.Level.ToString(), value);
        get => PlayerPrefs.GetInt(PrefKey.Level.ToString(), 1);
    }
    
}
