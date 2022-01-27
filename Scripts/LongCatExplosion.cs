using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongCatExplosion : MonoBehaviour
{

    //instantiates explosions in given transform positions


    public List<Transform> transforms;
    public GameObject explosion;
    

    private void OnDestroy() {
        foreach(Transform transform in transforms) {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
