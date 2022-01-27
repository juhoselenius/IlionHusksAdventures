using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public LeaderBoardDisplay[] highScoreDisplayArray;
    List<HighScoreEntry> scores = new List<HighScoreEntry>();
    //Adds highscore saved in PlayerPrefs
    void Start()
    {
        AddNewScore (PlayerPrefs.GetInt("highScore"));
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        scores.Sort((HighScoreEntry x, HighScoreEntry y) => y.scores.CompareTo(x.scores));

        for (int i = 0; i < highScoreDisplayArray.Length; i++)
        {
            if (i < scores.Count)
            {
                highScoreDisplayArray[i].DisplayHighScore(scores[i].scores);
            }
            else
            {
                highScoreDisplayArray[i].HideEntryDisplay();
            }
        }
    }


    void AddNewScore(int highScore)
    {
        scores.Add(new HighScoreEntry { scores = highScore });
    }
}