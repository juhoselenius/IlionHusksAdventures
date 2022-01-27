using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardDisplay : MonoBehaviour
{
    public Text scoreText;
    //Shows highscore in game
    public void DisplayHighScore( int scores)
    {
        scoreText.text = string.Format("{0:000000}", scores);
    }

    public void HideEntryDisplay()
    {
        scoreText.text = "";
    }
}