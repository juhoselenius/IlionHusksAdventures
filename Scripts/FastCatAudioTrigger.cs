using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastCatAudioTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioManager manager;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "FastEnemy(Clone)") {
            manager.Play("FastCatMovement");
        }
    }
}
