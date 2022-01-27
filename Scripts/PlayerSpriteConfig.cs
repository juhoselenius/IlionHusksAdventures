using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteConfig : MonoBehaviour
{
   
    //controls which sprites are showing when player moves

    public GameObject regular;
    public GameObject fromTop;
    public GameObject fromBot;
    GameObject audiomanager;


    void Start()
    {
        regular.SetActive(true);
        fromTop.SetActive(false);
        fromBot.SetActive(false);
        audiomanager = GameObject.Find("AudioManager");
    }

    // Update is called once per frame
    void Update()
    {
        vertical();
        horizontal();
        audioMovement();
    }


    //plays player movemnt audio
    private void audioMovement() {
        if (Input.GetKeyDown(KeyCode.W)) {
            audiomanager.GetComponent<AudioManager>().Play("PlayerMovement");
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            audiomanager.GetComponent<AudioManager>().Play("PlayerMovement");
        } else if (Input.GetKeyDown(KeyCode.A)) {
            audiomanager.GetComponent<AudioManager>().Play("PlayerMovement");
        } else if (Input.GetKeyDown(KeyCode.D)) {
            audiomanager.GetComponent<AudioManager>().Play("PlayerMovement");
        }
    }


    //switches between sprites
    private void vertical() {
        var input = Input.GetAxisRaw("Vertical");
        if (input == 0) {
            regular.SetActive(true);
            fromTop.SetActive(false);
            fromBot.SetActive(false);
        }
        else if (input == 1) {
            regular.SetActive(false);
            fromTop.SetActive(false);
            fromBot.SetActive(true);
        }
        else if (input == -1) {
            regular.SetActive(false);
            fromTop.SetActive(true);
            fromBot.SetActive(false);
        }
    }

    //makes the sprites tilt
    private void horizontal() {
        var input = Input.GetAxisRaw("Horizontal");
        if(input == 0) {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if (input == 1) {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -10);
        }
        else if (input == -1) {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 10);
        }
    }
}
