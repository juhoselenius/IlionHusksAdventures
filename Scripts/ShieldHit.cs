using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHit : MonoBehaviour
{

    //destroys object after certain amount of time
    float timer;
    public float maxTime;
    void Start() {
        timer = 0;
    }

    void Update() {
        timer += Time.deltaTime;
        if (timer > maxTime) {
            Destroy(gameObject);
        }
    }
}
