using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*This script is used for player's bullets
*/
public class BulletStats : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;
    public int Damage;
    Rigidbody2D rb;
    GameObject audiomg;
    public GameObject exp;

    void Awake()
    {
        audiomg = GameObject.Find("AudioManager");
        audiomg.GetComponent<AudioManager>().Play("PlayerShot");
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        {
            rb.AddForce(new Vector2(Speed, 0)); //gives speed and direction for bullets
        }
       

    }

    private void OnTriggerEnter2D(Collider2D collision) //declares what happens when bullet interacts with enemies or other objects
    {
        if (collision.gameObject.tag == "DestroyP")
        {
            Destroy(this.gameObject);

        } else if (collision.gameObject.tag == "Enemy") {
            Instantiate(exp, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
       
    }
}
