using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider Slider;


    // Start is called before the first frame update
    // Checks if there is previous audio volume saved
    void Start()
    {
        if (PlayerPrefs.HasKey("AudioVolume"))
        {
            PlayerPrefs.SetFloat("AudioVolume", 1);
            Load();
        }

        else
        {
            Load();
        }
    }
    //Saves and loads audio level changes made with slider
    public void ChangeVolume()
    {
        AudioListener.volume = Slider.value;
        Save();
    }
    private void Load()
    {
        Slider.value = PlayerPrefs.GetFloat("AudioVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("AudioVolume", Slider.value);
    }

}
