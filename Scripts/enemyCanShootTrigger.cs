using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCanShootTrigger : MonoBehaviour
{

    //makes the enemies shoot after they enter
    //this way enemies don*t start imediatly shooting after spawning

    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            AEnemy script;
            Debug.Log(collision.gameObject.name);
            if(collision.gameObject.GetComponent<AEnemy>() != null) {
                script = collision.gameObject.GetComponent<AEnemy>();
            }
            else {
                script = collision.gameObject.GetComponentInParent<AEnemy>();
            }


            if (script.shouldShoot) {
                script.canShoot = true;
                Debug.Log(collision.gameObject + "can shoot");
            }
        } 
    }
}
