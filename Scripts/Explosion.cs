using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
   
    //Destroys object after x amount of time

    float timer;
    public float maxTime;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > maxTime) {
            Destroy(gameObject);
        }
    }
}
