using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExperienceSystem : MonoBehaviour
{
    public int currentExp;
    public int currentLvlMaxExp;
    public int currentLvl;
    public int nextLvlMaxExp;

    Dictionary<int, int> levelxp = new Dictionary<int, int>();
    // Start is called before the first frame update
    void Start()
    {
        LoadXpLvl();
    }

    public void AddExp(int xp) {
        if(currentExp + xp >= currentLvlMaxExp) {
            int bonusXp = (currentExp + xp) - currentLvlMaxExp;
            if(bonusXp > 0) {
                currentExp = bonusXp;
                currentLvlMaxExp = nextLvlMaxExp;
                FindNewMaxExp();
                currentLvl++;
            }
            else {
                currentExp = 0;
                currentLvlMaxExp = nextLvlMaxExp;
                FindNewMaxExp();
                currentLvl++;
            }
        }
        else {
            currentExp += xp;
        }
    }

    private void FindNewMaxExp() {
        int newIndex = currentLvl + 1;
        var newxp = levelxp[newIndex];
        nextLvlMaxExp = newxp;
    }

    public void AddLevelXp(int level, int xp) {
        if (!levelxp.ContainsKey(level)) {
            levelxp.Add(level, xp);
        }
        else {
            levelxp.Remove(level);
            levelxp.Add(level, xp);
        }
    }

    public void SaveXPLvl() {
        PlayerPrefs.SetInt("currentxp", currentExp);
        PlayerPrefs.SetInt("currentlvl", currentLvl);
    }

    public void LoadXpLvl() {
        if (PlayerPrefs.HasKey("currentxp") && PlayerPrefs.HasKey("currentlvl")) {
            currentExp = PlayerPrefs.GetInt("currentxp");
            currentLvl = PlayerPrefs.GetInt("currentlvl");
            currentLvlMaxExp = levelxp[currentLvl];
            nextLvlMaxExp = levelxp[currentLvl + 1];
        }
        else {
            currentExp = 0;
            currentLvl = 0;
            currentLvlMaxExp = levelxp[currentLvl];
            nextLvlMaxExp = levelxp[currentLvl + 1];
        }
    }
}
