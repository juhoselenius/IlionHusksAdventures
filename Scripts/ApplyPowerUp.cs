using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPowerUp : MonoBehaviour
{
    //power up prefabs code
    //applies power up to player when player contacts the object

    public int color;
    public string powerUp;
    public float timer;
    GameObject gamemanager;
    GameObject audiomg;
    Rigidbody2D rb;
    public float speed;
    public List<Sprite> sprites = new List<Sprite>();
    private void Start() {
        gamemanager = GameObject.Find("GameManager");
        audiomg = GameObject.Find("AudioManager");
        ApplyColor(color);
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    //collider checks for impacts and deals with them accordingly
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "CombatCircle")
        {
            Debug.Log("got powerrrrrrr");
            gamemanager.GetComponent<PlayerManager>().ApplyPowerUp(name);
            audiomg.GetComponent<AudioManager>().Play("PowerUp");
            Destroy(gameObject);
        } else if(collision.gameObject.tag == "Destroy") {
            Destroy(gameObject);
        }
    }

    //moves the power up left and controlls live time for power ups
    private void Update() {
        timer -= Time.deltaTime;
        if(timer < 0) {
            Destroy(gameObject);
        }
        rb.AddForce(new Vector2(-1, 0) * speed);
    }


    //applies sprite to power up based on which one it is
    private void ApplyColor(int nr) {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        switch (nr) {
            case 0:
                renderer.sprite = sprites[0];
                break;
            case 1:
                renderer.sprite = sprites[1];
                break;
            case 2:
                renderer.sprite = sprites[2];
                break;
            case 3:
                renderer.sprite = sprites[3];
                break;
            case 4:
                renderer.sprite = sprites[4];
                break;
        }
           
    }


}
