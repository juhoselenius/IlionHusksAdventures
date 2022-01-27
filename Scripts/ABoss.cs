using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*This is scritpt for level 1 boss. This script contains boss' in scene mechanics 
 * notably where it will move and what is it's attack cycle
*/

public class ABoss : MonoBehaviour
{
    public int Health;
    public float Speed;
    public int Armor;
    public int Damage;
    public int scorePoints;
    public int hitDamagae;
    public float attackDelay;
    public int powerUpChance;
    public int powerUpType;
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject bullet3;
    private Rigidbody2D rb;
    private Collider2D coll;
    private GameObject player;
    public bool shouldShoot;
    GameObject GameManager;
    GameObject audioManager;
    private BossScript boss;
    private float spawnTimer = 0;
    public float activationTime;
    private float timer;
    private float movTimer;
    private Vector2 vector;
    private Vector3 center;
    private bool isAtPos;
    private int deathBossAtt = 1;
    private bool turned = false; //false for front, true for back
    private bool delay = true;
    private bool flickerStatus = false;
    private float flickerTimer;
    private int mouthCounter = 0;

    // Start is called before the first frame update
    void Awake()
    {
        //finds necessary components

        GameManager = GameObject.Find("GameManager");
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        player = GameObject.Find("PlayerPos");
        audioManager = GameObject.Find("AudioManager");
  
        boss = new BossScript(bullet, bullet2, bullet3); 


        timer = 0; 
        movTimer = 0;
        isAtPos = false;


    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        //Debug.Log("Boss timer: " + spawnTimer);
        if(spawnTimer > activationTime) //activationTime = when boss starts moving
        {
            BossMovement(7f, 0f, Speed); //coordinates to where boss will move at the beginning
        }






        if (isAtPos) //starts shooting when arrives at it's location
        {
            if (flickerStatus && Time.time > flickerTimer)
            {
                flickerStatus = boss.Flicker(turned, flickerStatus); //this is to change back to normal sprite after "flickering" when shot
            }
            BossShoot();
            
            


            
        }


        








    }

    private void OnTriggerEnter2D(Collider2D collision) //what happens when colliding other objects
    {
        if (collision.gameObject.tag == "Destroy")
        {
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.tag == "Bullet")
        {
            if (isAtPos)  //only takes damage after boss has arrived at it's destination
            {
                TakeDamage(collision.gameObject.GetComponent<BulletStats>().Damage);
                flickerStatus = boss.Flicker(turned, flickerStatus);  //flickers when shot (changes sprite)
                flickerTimer = Time.time + 0.1f; //time to delay changing back to normal sprite
            }

        }
        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit player");
            GameManager.GetComponent<PlayerManager>().TakeDamage(hitDamagae);
        }
    }

    public void TakeDamage(int dmg) //applies damage on boss
    {
        if (Health - dmg <= 0)
        {
            GameManager.GetComponent<PlayerManager>().AddScore(scorePoints);
            audioManager.GetComponent<AudioManager>().Play("Destroy");
            GameManager.GetComponent<PowerUpSpawner>().SpawnRoulette(powerUpChance, gameObject.transform.position, powerUpType);
            Destroy(this.gameObject);
        }
        Health -= dmg;
        audioManager.GetComponent<AudioManager>().Play("Bosshit");
    }


    private void BossShoot() //alternates between 4 attacks
    {





        if (delay) //check if delay is needed
        {
            timer = (Time.time + 3f);
            delay = false;
        }

        if (deathBossAtt == 6 && Time.time < timer) //if case = 6 do buttAttack until time reaches timer
        {
            boss.ButtAttack();
        }
        else if (deathBossAtt == 6 && Time.time > timer) //when time reaches timer case = 1 -> start new cycle
        {
            deathBossAtt = 1;
        }

        if (Time.time > timer) //delay between attacks
        {

            switch (deathBossAtt)
            {
                case 1:

                    if (turned) //if backside is up turn to frontside
                    {
                        turned = boss.Turn();
                        delay = true; //add delay
                        break;
                    }
                    boss.ConeAttack(); //otherwise do coneattack
                    timer = (Time.time + attackDelay); //attack delay
                    deathBossAtt = 2;
                    break;
                case 2:
                    boss.ConeAttack(); //another coneattack
                    timer = (Time.time + attackDelay);
                    delay = true; //add delay
                    deathBossAtt = 3;
                    break;
                case 3:
                    boss.MouthAttack(); //then mouthattack
                    timer = (Time.time + attackDelay);
                    mouthCounter += 1;
                    if (mouthCounter >= 3)
                    {
                        delay = true; //add delay
                        deathBossAtt = 4;
                        mouthCounter = 0;
                        break;
                    }
                    
                    

                    break;
                case 4:

                    turned = boss.Turn(); //turn to backside
                    timer = (Time.time + attackDelay);
                    delay = true; //add delay
                    deathBossAtt = 5;


                    break;
                case 5:
                    timer = (Time.time + 5f); //add value for how long the buttattack will be in seconds
                    deathBossAtt = 6;
                    break;

            }

        }





    }

    private void RandomMovement(float xmin, float xmax, float ymin, float ymax, float changeDelay) //makes object move in a random direction between given coordinates ^^this method is not used at the moment but i decided to leave it here
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

        else if (Time.time > movTimer) //changes direction to a random direction once per given timeframe (changeDelay)
        {


            vector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rb.velocity = vector * Speed * 0.5f;





            movTimer = (Time.time + changeDelay);
        }



    }

    private void BossMovement(float xCoord, float yCoord, float giveSpeed) //starts moving towards position (x,y) and after arriving starts moving up and down randomly
    {
        isAtPos = boss.BossMoveTo(xCoord, yCoord, giveSpeed);  //start moving towards position (x,y), returns true if is at position on x-axis


        if (Time.time > movTimer)  //changes direction on y-axis based on delay established by movTimer
        {

            if (isAtPos)//only moves up and down when reaches given x-coordinate
            {

                boss.UpDownMovement(true, giveSpeed); //changes direction(up,down)/speed randomly
                
            }
            movTimer = (Time.time + 2f); //change the float value for bigger/smaller delay between changing direction
        }
        else
        {

            if (isAtPos)
            {

                boss.UpDownMovement(false, giveSpeed); // (when false) checks if boss is going out of bounds and corrects movement accordingly
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.GetComponent<PlayerManager>().SaveNextLevel();
        
        //Tells SceneChanger that boss is dead and checks spawn manager if textBoxActivator needs to be spawned.
        if(GameObject.Find("SceneChanger").GetComponent<SceneTransition>() != null)
        {
            GameObject.Find("SceneChanger").GetComponent<SceneTransition>().isBossDead = true;
        }
        if (GameObject.Find("GameManager").GetComponent<EnemySpawnManager>() != null && GameObject.Find("GameManager").GetComponent<EnemySpawnManager>().spawnTextBoxWhenBossDestroyed)
        {
            GameObject.Find("GameManager").GetComponent<EnemySpawnManager>().SpawnTextBoxActivator();
        }
    }
}
