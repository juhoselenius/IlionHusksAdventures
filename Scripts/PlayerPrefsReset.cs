using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsReset : MonoBehaviour
{
    // resets playerprefs
    void Start()
    {
        PlayerPrefs.SetInt("currentLvl", 0);
        PlayerPrefs.SetInt("currentScore", 0);
    }
}
