using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombatCircle : MonoBehaviour
{
    //shield that destroys enemies and bullets 



    public float expansionTime;
    private float timer;
    public int damage;
    public float maxX;
    public float shieldTime;
    public GameObject hit;
    AudioManager audiomg;

    //sclaes down sprite and plays sound
    private void Awake() {
        this.gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        audiomg = FindObjectOfType<AudioManager>();
        audiomg.Play("ShieldBlast");
    }

    //makes the sprite scale up untill a certain point and destroys the object after max time
    private void Update() {
        timer += Time.deltaTime;
        for(float i = 0f; i < expansionTime; i += Time.deltaTime) {
            if(gameObject.transform.localScale.x < maxX) {
                this.gameObject.transform.localScale += new Vector3(0.0005f, 0.0005f, 0.0005f);
            }

        }

        

       if(timer > shieldTime) {
            Destroy(gameObject);
       }
    }

    //checks for impact and deals accordingly
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<AEnemy>().TakeDamage(damage);
            if(collision.gameObject.GetComponent<AEnemy>() == null) {
                collision.gameObject.GetComponentInChildren<AEnemy>().TakeDamage(damage);
            }
            Instantiate(hit, collision.gameObject.transform.position, Quaternion.identity);
            
        } else if (collision.gameObject.tag == "EnemyBullet") {
            Instantiate(hit, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision) {

        if(collision.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }*/

}
