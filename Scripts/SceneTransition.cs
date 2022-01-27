using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class SceneTransition changes the scenes of the game from one to another.
/// </summary>

public class SceneTransition : MonoBehaviour
{
    [SerializeField] string nextScene;
    [SerializeField] float sceneTime;
    private float timeElapsed;
    [SerializeField] string sceneType;
    [SerializeField] string thisSceneMusic;
    [SerializeField] bool stopPreviousMusic = false;
    private AudioManager sceneAudioManager;
    public bool isBossDead = false;
    public float textActivatorTimer;

    // Start is called before the first frame update
    void Start()
    {
        sceneAudioManager = FindObjectOfType<AudioManager>();
        if(sceneAudioManager != null)
        {
            if (stopPreviousMusic)
            {
                sceneAudioManager.StopPlayingAllSounds();
            }

            if(thisSceneMusic != null)
            {
                Debug.Log(SceneManager.GetActiveScene().name + " scene starts playing sound: " + thisSceneMusic);
                sceneAudioManager.Play(thisSceneMusic);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBossDead)
        {
            textActivatorTimer += Time.deltaTime;
            Debug.Log("Activation timer:" + textActivatorTimer);
        }
        if (sceneType.Equals("timed"))
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > sceneTime)
            {
                SceneManager.LoadScene(nextScene);
            }
        }

        if(sceneType.Equals("pressKey"))
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(nextScene);
            }
        }

        // waitForReady has to confirm that there is no active text box after the specified time (sceneTime).
        if (sceneType.Equals("waitForReady"))
        {
            if(!GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>().isActive)
            {
                timeElapsed += Time.deltaTime;


                if(timeElapsed > sceneTime)
                {
                    SceneManager.LoadScene(nextScene);
                }
            }
        }

        // waitForBoss has to have confirmed that boss is dead and that there is no active text box after the specified time (sceneTime).
        if(sceneType.Equals("waitForBoss"))
        {
            if(isBossDead)
            {
                timeElapsed += Time.deltaTime;
                if (!GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>().isActive)
                {
                    if (timeElapsed > sceneTime)
                    {
                        SceneManager.LoadScene(nextScene);
                    }
                }
            }
        }
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(nextScene);
    }
}
