using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
/*This is script for level 2 boss. it mainly controls how and when boss moves
 * and attacks
*/

public class BBoss : MonoBehaviour
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
    public bool canShoot;
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
    private SecondBossScript boss;
    private float spawnTimer = 0;
    public float activationTime;
    private float timer;
    private bool isAtPos;
    private int BossAtt = 1;
    private bool turned = false; //false for front, true for back
    private bool delay = true;
    private bool faded = false;
    private int eyecounter = 0;
    private int lazerCounter = 0;
    private int cycle = 5;
    private float flickerTimer;
    private bool flickerStatus = false;
    private bool fading = false;

    // Start is called before the first frame update
    void Awake()
    {
        //get necessary components
        GameManager = GameObject.Find("GameManager");
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        player = GameObject.Find("PlayerPos");
        audioManager = GameObject.Find("AudioManager");

        boss = new SecondBossScript(bullet, bullet2, bullet3);


        timer = 0;
        isAtPos = false;


    }

    // Update is called once per frame
    void Update()
    {

        spawnTimer += Time.deltaTime;
        //Debug.Log("Boss timer: " + spawnTimer);
        if (spawnTimer > activationTime) //activationtime = when boss starts moving
        {
            Boss1Movement(3f, 0f, Speed); //coordinates to where boss will move at the beginning
        }
        


        if (canShoot)
        {

            


            if (isAtPos)//starts shooting when arrives at target x-coordinate
            {
                if (flickerStatus && Time.time > flickerTimer) 
                {
                    flickerStatus = boss.Flicker(turned, flickerStatus); //this is to change back to normal sprite after "flickering" when shot
                }




                BossShoot();
            }
            







        }


    }

    private void OnTriggerEnter2D(Collider2D collision) //what happens when colliding whit other objects
    {
        if (collision.gameObject.tag == "Destroy")
        {
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.tag == "Bullet")
        {
            if (isAtPos)
            {
                if (!fading) //can't be damaged while doing fadeAttack
                {
                    TakeDamage(collision.gameObject.GetComponent<BulletStats>().Damage);
                    flickerStatus = boss.Flicker(turned, flickerStatus); //flickers when shot(changes sprite)
                    flickerTimer = Time.time + 0.1f;//delay to change sprite when "flickering"
                }
                
            }
            
        }
        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit player");
            GameManager.GetComponent<PlayerManager>().TakeDamage(hitDamagae);
        }
    }

    public void TakeDamage(int dmg)//method for taking damage
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

  
    private void BossShoot() //boss 2 mechanics
    {


        
        

        if (delay) //check if delay is needed
        {
            timer = (Time.time + 3f);
            delay = false;
        }

        
        
        

        if (Time.time > timer) //delay between attacks
        {

            switch (BossAtt)
            {
                case 1:
                    if (!turned)
                    {
                        turned = boss.Turn();  //turn red
                    }
                    boss.LazerAttack(); //do  5 lazerattacks with 1 s delay
                    timer = (Time.time + 1f);
                    lazerCounter += 1;
                    if (lazerCounter >= cycle) //does eyeattack 5 times + cycle
                    {
                        turned = boss.Turn();//turns back to normal
                        delay = true;
                        BossAtt = 2;
                        lazerCounter = 0; //resets lazercounter for next cycle
                        break;
                    }

                    break;
                
                case 2:
                    boss.EyeAttack(); //do eyeattack
                    timer = (Time.time + attackDelay);
                    eyecounter += 1; 
                    if(eyecounter >= cycle) //does eyeattack 5 times + cycle
                    {
                        delay = true; //add delay
                        BossAtt = 3;
                        cycle += 1;
                        eyecounter = 0; //resets eyecounter for next cycle
                        break;
                    }

                    break;
                case 3:
                    
                    timer = (Time.time + attackDelay); //delay
                    delay = true;
                    BossAtt = 4;
                    break;


                case 4:
                    faded = boss.FadeAttack(); //fades
                    fading = true;
                    if (faded)
                    {
                        boss.Teleport(); //after faded teleports
                        BossAtt = 5;
                        break;
                    }
                    break;

                case 5:
                    faded = boss.FadeBack(); //then fades back
                    if (!faded)
                    {
                        BossAtt = 1; //when faded back starts cycle from beginning
                        delay = true;
                        fading = false;
                        break;
                    }
                    
                    break;
            }

        }





    }



    private void Boss1Movement(float xCoord, float yCoord, float giveSpeed) //starts moving towards position (x,y) and after arriving starts moving up and down randomly
    {
        if (!isAtPos)
        {
            isAtPos = boss.BossMoveTo(xCoord, yCoord, giveSpeed);  //start moving towards position (x,y), returns true if is at position on x-axis
            if (isAtPos)
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        


    }

    private void OnDestroy()
    {
        //Tells SceneChanger that boss is dead and checks spawn manager if textBoxActivator needs to be spawned.
        if (GameObject.Find("SceneChanger").GetComponent<SceneTransition>() != null)
        {
            GameObject.Find("SceneChanger").GetComponent<SceneTransition>().isBossDead = true;
        }
        if (GameObject.Find("GameManager").GetComponent<EnemySpawnManager>() != null && GameObject.Find("GameManager").GetComponent<EnemySpawnManager>().spawnTextBoxWhenBossDestroyed)
        {
            GameObject.Find("GameManager").GetComponent<EnemySpawnManager>().SpawnTextBoxActivator();
        }
    }
}
