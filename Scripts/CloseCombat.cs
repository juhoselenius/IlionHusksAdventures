using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseCombat : MonoBehaviour
{
    //instantiates close combar shield that damages enemies and destroys enemy bullets



    public GameObject ccc;
    public float cooldownTimer;
    public float timer;


    private void Start() {
        timer = 0;
    }

    //simple instantiate with cooldowntimer
    
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 vec = gameObject.transform.position;
        var vec2 = new Vector3(vec.x, vec.y + 0.3f, vec.z);
        if(timer > 0) {

            if (Input.GetKeyDown(KeyCode.R)) {
                Instantiate(ccc, vec2, Quaternion.identity);
                timer = 0;
                timer -= cooldownTimer;
            }
        }
    }
}
