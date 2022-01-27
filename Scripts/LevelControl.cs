using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    //this script add score based on passed time
    public PlayerManager playerManager;
    float lastUpdate = 0;


    void Update()
    {
        AddScoreByTime();
    }

    //adds score by time
    private void AddScoreByTime() {

        if (Time.time - lastUpdate >= 1f) {
            playerManager.AddScore(1);
            lastUpdate = Time.time;
        }

    }
}
