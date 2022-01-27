using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/* This class manages all the sounds of the game.
 * Based on "Introduction to AUDIO in Unity" tutorial by Brackeys (https://www.youtube.com/watch?v=6OT43pvUyfY).
 */

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    //public string sceneToStart;
    //public string sceneWhereDestroyed;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            return;
        }
        s.source.Play();
    }


    public void StopPlay(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) {
            return;
        }
        Debug.Log(SceneManager.GetActiveScene().name + " scene stops (StopPlay) sound: " + s.source.clip.name);
        s.source.Stop();
    }

    public void StopPlayingAllSounds()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.source.isPlaying)
            {
                Debug.Log(SceneManager.GetActiveScene().name + " scene stops (StopPlayingAllSounds) sound: " + sound.source.clip.name);
                sound.source.Stop();
            }
        }
    }
}
