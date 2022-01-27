using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This is a script thats given to every enemy's bullet object and it dictates how the bullet behaves
*/
public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;
    public int Damage;
    Rigidbody2D rb;
    public bool bouncy; 
    public int homManyBounce; //how many times bullet bounces from screen edges before it is destroyed
    public bool straight;
    public bool towards;
    public bool homing;
    public bool sin;
    public float amplitude; 
    public float wavelength;
    private GameObject player;
    private Vector3 direction;
    public PlayerManager playerManager;
    public GameObject gameManager;
    public bool grow;
    public float growth;
    public float growTime;
    public bool canBeShot;
    public int howManyShots;
    private float timer;
    private int bounced = 0;
    private float growTimer = 1000;
    private bool first = true;
    private int shotCounter = 0;
    public GameObject hitAnimation;



    void Start()
    {
        //get necessary components
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("PlayerPos");
        gameManager = GameObject.Find("GameManager");
        playerManager = gameManager.GetComponent<PlayerManager>();
        //get direction towards player
        direction = (Vector3)(rb.transform.position - player.transform.position);
        direction.Normalize();
       
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        
        Move();
        if (grow) //make bullet grow if it needs to
        {
            
            if(Time.time < growTimer) //only grows until growtimer is reached 
            {
                Vector3 scaleChange = new Vector3(growth, growth, 0); //https://docs.unity3d.com/ScriptReference/Transform-localScale.html 
                rb.transform.localScale += scaleChange;
                if (first)
                {
                    growTimer = Time.time + growTime; //when this is first called initialize growtimer with value growTime given for the object 
                    first = false;
                }
                
            }
            
            
            
        }
        
       

    }

    private void OnTriggerEnter2D(Collider2D collision) //destroys bullets when they collide with other gameobjects
    {
        if (collision.gameObject.tag == "Destroy")
        {
            Destroy(this.gameObject);

        } else if (collision.gameObject.tag == "Player"){
            Debug.Log("Hit player");
            playerManager.TakeDamage(Damage); //make player take damage
            DestroyBullet();
        }
        else if( collision.gameObject.tag == "Wall")
        {
            if (!bouncy) //bouncy bullets have their own mechanics so they won't be destroyed here
            {
                Destroy(this.gameObject);
            }
            
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            if (canBeShot) //if canBeShot is enabled player can destroy this object by shooting it
            {
                shotCounter += 1;
                if(shotCounter >= howManyShots)
                {
                    DestroyBullet();
                }
                
            }
        }

    }
    private void Move() //bullet movement mechanics
    {
        //straight line to left
        if (straight) 
        {
            rb.AddForce(new Vector2(-1 * Speed, 0));
        }
        //straight towards player
        else if (towards) 
        {


            rb.transform.right = new Vector2(direction.x, direction.y);

            rb.AddForce(-direction * Speed);


        }
        //homing to player/follows player 
        else if (homing) 
        {
            float distance = Speed * Time.deltaTime;
            rb.transform.position = Vector3.MoveTowards(rb.transform.position, player.transform.position, distance);

        }
        else if (sin) //y = sin(x)
        {
            
            Vector2 vector = new Vector2(-1 * Speed, amplitude * Mathf.Sin(Time.time * wavelength));
            rb.velocity = vector;

        }

        else if (bouncy)  //bounces off of map edges (needs to have starting velocity)
        {
          
            if (Time.time > timer) //check for delay
            {
                if (rb.transform.position.y >= 4.9f || rb.transform.position.y <= -4.9)  //chage velocity according to what wall bullet bounces from
                                                                                           //for top and bottom new vector = -old vector + 2 * old vector x component
                                                                                           //for left and right new vector = -old vector + 2 * old vector y component
                {           

                    Vector2 vector = rb.velocity;


                    rb.transform.right = (Vector2)(-vector + new Vector2(2 * vector.x, 0)); 
                    rb.velocity = (Vector2)(-vector + new Vector2(2 * vector.x, 0));
                    bounced += 1;
                    Delay(0.1f);




                }
                else if (rb.transform.position.x <= -8.8 || rb.transform.position.x >= 8.8)
                {
                    Vector2 vector = rb.velocity;


                    rb.transform.right = (Vector2)(-vector + new Vector2(0, 2 * vector.y));
                    rb.velocity = (Vector2)(-vector + new Vector2(0, 2 * vector.y));


                    bounced += 1;
                    Delay(0.1f);

                }
             
            }



            
           if(rb.transform.position.x < -9.5 || rb.transform.position.x > 9.5 || rb.transform.position.y > 5.5 || rb.transform.position.y < -5.5) //for incurance if somehow bullet gets too far change direction towards centre
            {
                
                Vector2 vector = rb.velocity;
                rb.velocity = -vector;

                Vector2 centre = (Vector2)(-rb.transform.position).normalized;
                rb.transform.right = centre;
                rb.velocity = centre * 5;
                bounced += 1;
            }

           if(bounced > homManyBounce)
            {
                DestroyBullet();
            }

           
        }
        
    }
    private void Delay(float delayTime)
    {
        timer = Time.time + delayTime;
    }

    private void DestroyBullet()
    {
        Instantiate(hitAnimation, rb.transform.position, Quaternion.identity);
        Destroy(this.gameObject);

    }

   
    

    
  
}
