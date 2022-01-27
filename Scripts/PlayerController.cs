using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float fireRate;
    private float lastAtt = -100;
    public GameObject bullet;
    GameObject gun1;
    GameObject gun2;
    private int i = 0;
    public GameObject flash;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        gun1 = GameObject.Find("gun1");
        gun2 = GameObject.Find("gun2");

    }
    void Update()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
        rb.AddForce(new Vector2(0, Input.GetAxis("Vertical") * speed));
        if (Input.GetKey(KeyCode.Space))
        {
            shoot();
        }
    }
    private void shoot()
    {
        gun1 = GameObject.Find("gun1");
        gun2 = GameObject.Find("gun2");
        if (Time.time > lastAtt)
        {
            if(i == 0)
            {
                var bull = Instantiate(bullet, gun1.transform.position, Quaternion.identity);
                Instantiate(flash, gun1.transform.position, Quaternion.identity);
                bull.SetActive(true);
                lastAtt = Time.time + fireRate;
                i = 1;
          
            }else if(i == 1)
            {
                var bull2 = Instantiate(bullet, gun2.transform.position, Quaternion.identity);
                Instantiate(flash, gun2.transform.position, Quaternion.identity);
                bull2.SetActive(true);
                lastAtt = Time.time + fireRate;
                i = 0;
            }
        }
    }
}
