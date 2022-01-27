using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    //script that makes sure colliders are off when gameobject is disabled
    public Collider2D coll;
   

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf) {
            coll.enabled = true;
        } else {
            coll.enabled = false;
        }
    }
}
