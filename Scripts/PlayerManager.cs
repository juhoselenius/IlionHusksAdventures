using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class PlayerManager : MonoBehaviour
{

    //this script controlls the playercontroller
    //its purpose is to save stats and apply information
    // Start is called before the first frame update
    public int currentScore;
    public int currentLevel;
    public int currentHealth;
    public int currentArmor;
    public PlayerStats[] stats;
    public PowerUp[] powerUps;
    public PlayerController playerScript;
    public HealthBar healthBar;
    public ShieldManager shield;
    public TMP_Text text;
    GameObject audiomanager;
    GameOverManager gameOver;
    bool powerupOn = false;
    float powerTime;
    float normalSpeed;
    float normalFireRate;
    void Awake()
    {
        LoadStats();
        audiomanager = GameObject.Find("AudioManager");
        gameOver = GetComponent<GameOverManager>();
        Debug.Log(PlayerPrefs.GetInt("currentLvl"));
    }

    //this method adds scorepoints to currentScore and updates the UI scorepoints
    public void AddScore(int score) {
        currentScore += score;
        text.text = currentScore.ToString();
    }

    //this method saves the currentscore to highscore if currentscore is high enough
    public void Save() {
        if (currentScore > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", currentScore);
            Debug.Log("Save");
        }
        else { 
            Debug.Log("No highscore");
        }
    }
    
    //this method resets the playerprefs
    public void ResetLevel() {
        PlayerPrefs.SetInt("currentLvl", 0);
        PlayerPrefs.SetInt("currentScore", 0);
    }

    //loads playerprefs currentscore
    public void LoadScore() {
        currentScore = PlayerPrefs.GetInt("currentScore");
    }

    //sets up next level
    public void SaveNextLevel() {
        PlayerPrefs.SetInt("currentLvl", (currentLevel + 1));
        PlayerPrefs.SetInt("currentScore", currentScore);
    }

    //loads playerprefs currentLvl
    public void LoadStats() {
        currentLevel = PlayerPrefs.GetInt("currentLvl");
        AddStats(currentLevel);
    }


    //adds stats from List to playercontroller
    private void AddStats(int level) {
        if (stats[level] != null) {
            playerScript.speed = stats[level].Speed;
            playerScript.fireRate = stats[level].FireRate;
            normalFireRate = stats[level].FireRate;
            normalSpeed = stats[level].Speed;
            currentArmor = stats[level].Armor;
            currentHealth = stats[level].Health;
            healthBar.SetMaxHealth(currentHealth);
            shield.SetMaxArmor(currentArmor);
            if (level > 0) {
                LoadScore();
            }
        }
        else {
            Debug.Log("No stats");
        }
    }

    //controlls the powerup timer
    //updates UI
    private void Update() {
        if (powerupOn) {
            powerTime -= Time.deltaTime;
            if(powerTime < 0) {
                powerupOn = false;
            }
        }
        else {
            playerScript.fireRate = normalFireRate;
            playerScript.speed = normalSpeed;
        }
        healthBar.SetHealth(currentHealth);
        shield.SetArmor(currentArmor);

    }

    //takes powerup stats from list and applies them to player
    public void ApplyPowerUp(string name) {
        PowerUp powerUp = Array.Find(powerUps, power => power.name == name);
        if(powerUp == null) {
            return;
        }
        Debug.Log("Powerup name: " + name);
        currentHealth += powerUp.AddedHealth;
        currentArmor += powerUp.AddedArmor;
        playerScript.speed = powerUp.TemporarySpeed;
        playerScript.fireRate = powerUp.TemporaryFireRate;
        powerTime = powerUp.powerUpTime;
        if (!powerupOn) {
            powerupOn = true;
        }
    }

    //decreases health or armor and kills player if below 0
    public void TakeDamage(int damage) {
        if (currentArmor <= 0) {
            if (currentHealth - damage <= 0) {
                Save();               
                Debug.Log("Dead");
                currentHealth = 0;
                audiomanager.GetComponent<AudioManager>().Play("Destroy");
                playerScript.gameObject.SetActive(false);               
                gameOver.currentScore = currentScore;              
                gameOver.EndGame();
                Time.timeScale = 0f;
                

            }
            else {
                currentHealth -= damage;
                Debug.Log("Took Damage");
            }
        }
        else {
            if (currentArmor - damage < 0) {
                currentArmor = 0;
            }
            else {
                currentArmor -= damage;
            }
        }
        audiomanager.GetComponent<AudioManager>().Play("PlayerHit");
        
    }

}
