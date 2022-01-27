using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    
    //destroys the object after certain amount of time

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
