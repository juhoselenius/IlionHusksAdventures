using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    //this script checks the power ups and gives them to playermanager
    public List<int> powerupColor = new List<int>();
    public List<string> powerUpName = new List<string>();
    public List<float> powerUpTimer = new List<float>();
    public GameObject powerUp;


    //instantiates power up with specific traits
    public void SpawnRoulette(int chance, Vector3 position, int power) {
        Debug.Log("Rolling Dice");
        int nr = Random.Range(0, chance + 1);
        if(nr < 2) {
            var p = Instantiate(powerUp, position, Quaternion.identity);
            p.GetComponent<ApplyPowerUp>().color = powerupColor[power];
            p.GetComponent<ApplyPowerUp>().name = powerUpName[power];
            p.GetComponent<ApplyPowerUp>().timer = powerUpTimer[power];
        } 
    }

    //instantiates power up with specific traits
    //added chance
    public void SpawnRandomPower(int chance, Vector3 pos, bool luck) {
        int nr = Random.Range(0, chance + 1);
        if(nr < 2) {
            int roll = Random.Range(0, 99);
            if( roll > 79 || luck) //20% chance for last 2 items in list and 80% to roll any other
            {
                int j = Random.Range(powerUpName.Count - 2, powerUpName.Count);
                var p = Instantiate(powerUp, pos, Quaternion.identity);
                p.GetComponent<ApplyPowerUp>().color = powerupColor[j];
                p.GetComponent<ApplyPowerUp>().name = powerUpName[j];
                p.GetComponent<ApplyPowerUp>().timer = powerUpTimer[j];
            }
            else
            {
                int i = Random.Range(0, powerUpName.Count - 2);
                var p = Instantiate(powerUp, pos, Quaternion.identity);
                p.GetComponent<ApplyPowerUp>().color = powerupColor[i];
                p.GetComponent<ApplyPowerUp>().name = powerUpName[i];
                p.GetComponent<ApplyPowerUp>().timer = powerUpTimer[i];
            }
            

        }
    }

}
