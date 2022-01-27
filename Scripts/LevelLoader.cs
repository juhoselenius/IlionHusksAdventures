using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    //Loads level templates and instantiates them on screen


    public LevelScriptableObject[] levelSO;
    public GameObject[] levelTemplates;
    public LevelTemplateScript[] levelTscripts;
    public Button[] buttons;


    void Start()
    {
        for(int i = 0; i < levelSO.Length; i++) {
            levelTemplates[i].SetActive(true);
        }
    }

    private void LoadPanels() {
        for(int i = 0; i < levelSO.Length; i++) {
            levelTscripts[i].levelTxt.text = levelSO[i].level.ToString();
            levelTscripts[i].descriptiontxt.text = levelSO[i].difficulty;
            buttons[i].interactable = true;
        }
    }

    public void SelectLevel(int lvlNr) {
        SceneManager.LoadScene(lvlNr);
    }
}
