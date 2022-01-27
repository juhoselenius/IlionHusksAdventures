using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
/*This is basic script given to all normal enemies
 * it controls their movement and shooting and other behavior
*/

public  class AEnemy : MonoBehaviour
{
    public int Health;
    public float Speed;
    public int Armor;
    public int Damage;
    public int scorePoints;
    public int hitDamagae;
    public float attackDelay;
    public int powerUpChance;
    public bool luck; //if true gives always good powerup
    private float lastAtt;
    public bool canShoot; //declares if enemy can shoot
    public GameObject bullet; 
    public GameObject bullet2;
    public GameObject bullet3;
    public int movement; //0 for not moving, 1 for straight line, 2 for moving towards player
    private Rigidbody2D rb;
    private Collider2D coll;
    private GameObject player;
    public bool shouldShoot;
    GameObject GameManager;
    GameObject audioManager;
    private float movTimer;
    private Vector2 vector;
    private Vector3 center;
    public GameObject explosion;


    // Start is called before the first frame update
    void Awake()
    {
        //get necessary components
        GameManager = GameObject.Find("GameManager");
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        lastAtt = 0; //Time.time + attackDelay
        player = GameObject.Find("PlayerPos");
        audioManager = GameObject.Find("AudioManager");
        move(movement);
        
        movTimer = 0;


    }

    // Update is called once per frame
    void Update()
    {
        if (movement == 2)
        {
            move(movement);
        }
       
        

        if (canShoot)
        {

           
            
            Shoot();
            

        }


    }

    private void OnTriggerEnter2D(Collider2D collision) //what happens when a enemy collides with other gameobjects
    {
        if (collision.gameObject.tag == "Destroy")
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Player") 
        {
            Debug.Log("Hit player");
            GameManager.GetComponent<PlayerManager>().TakeDamage(hitDamagae);
            GameManager.GetComponent<PowerUpSpawner>().SpawnRandomPower(powerUpChance, gameObject.transform.position, luck);
            audioManager.GetComponent<AudioManager>().Play("Destroy");
            if (explosion != null) {
                Instantiate(explosion, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<BulletStats>().Damage);
        }
    }

    public void TakeDamage(int dmg) //take damage when shot
    {
        if (Health - dmg <= 0)
        {
            GameManager.GetComponent<PlayerManager>().AddScore(scorePoints);
            audioManager.GetComponent<AudioManager>().Play("Destroy");
            GameManager.GetComponent<PowerUpSpawner>().SpawnRandomPower(powerUpChance, gameObject.transform.position, luck);
            if (explosion != null) {
                Instantiate(explosion, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
        Health -= dmg;
        audioManager.GetComponent<AudioManager>().Play("SmallEnemyHit");
    }
    private void Shoot() //shoot with attackdelay
    {
        if(Time.time > lastAtt)
        {
            var bull = Instantiate(bullet, rb.transform.position, Quaternion.identity);
            bull.SetActive(true);
            if(SceneManager.GetActiveScene().name.Equals("Level1"))
            {
                audioManager.GetComponent<AudioManager>().Play("EnemyShot");
            }
            if (SceneManager.GetActiveScene().name.Equals("FinalLevel"))
            {
                audioManager.GetComponent<AudioManager>().Play("FinalLevelEnemyShot");
            }
            lastAtt = Time.time + attackDelay;
        }
    }

    private void move(int movementStyle)
    {
        switch (movementStyle)
        {
            case 0: 
                break;
            case 1:
                rb.AddForce(new Vector2(-1 * Speed, 0)); //move straight
                audioManager.GetComponent<AudioManager>().Play("EnemyMovement");
                break;
            case 2: //https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html  makes enemy go towards players position
                float distance = Speed * Time.deltaTime * 1.2f;
                rb.transform.position = Vector3.MoveTowards(rb.transform.position, player.transform.position, distance);
                if(Time.timeScale != 0)
                {
                    rb.AddForce(new Vector2(-1, 0) * Speed * 2); //remove this for completely homing enemy/projectile
                }
                
                break;


        }
        

    }



    




    private void RandomMovement(float xmin, float xmax, float ymin, float ymax, float changeDelay) //makes object move in a random direction between given coordinates  ^^this method is not used at the moment but i decided to leave it here
                                                                                                    //in case it was needed later^^
    {
        center = new Vector3((xmax + xmin) / 2, (ymax + ymin) / 2, 0); //vector that points to the middle of the given points

        if (rb.transform.position.x < xmin) //checks if the object is going out of bounds and corrects movement toward center if so
        {
            rb.velocity = new Vector2(0, 0);
            rb.velocity = -0.2f * (Vector3)(rb.transform.position - center);


        }
        else if (rb.transform.position.x > xmax)
        {
            rb.velocity = new Vector2(0, 0);
            rb.velocity = -0.2f * (Vector3)(rb.transform.position - center);
        }
        else if (rb.transform.position.y > ymax)
        {
            rb.velocity = new Vector2(0, 0);
            rb.velocity = -0.2f * (Vector3)(rb.transform.position - center);
        }
        else if (rb.transform.position.y < ymin)
        {
            rb.velocity = new Vector2(0, 0);
            rb.velocity = -0.2f * (Vector3)(rb.transform.position - center);
        }

        else if (Time.time > movTimer) //changes direction to a random direction once per given timeframe
        {
            
            
            vector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rb.velocity = vector * Speed * 0.5f;
            
            



            movTimer = (Time.time + changeDelay); 
        }



    }
    
  

}
