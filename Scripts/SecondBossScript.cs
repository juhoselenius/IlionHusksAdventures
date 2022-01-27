using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*This script contains mechanics for boss 2 that can be used 
 * in BBoss script. This script contains all methods for level 2 boss' attacs and movement
*/

public class SecondBossScript : MonoBehaviour
{
    private Rigidbody2D rbBullet;
    private GameObject bullet;
    private GameObject eye;
    private GameObject beeBullet;
    private GameObject Boss;
    private GameObject front;
    private GameObject back;
    private GameObject cloud;
    private Rigidbody2D rb;
    private Vector3 point;
    private int turn; //0 for front, 1 for back
    private SpriteRenderer sr;
    private float value = 0;
    private float value2 = 0;
    private Color bossColor;
    private SpriteRenderer frontSr;
    private Sprite bossSprite;
    private GameObject dmg;
    private Sprite dmgSprite;
    private SpriteRenderer backSr;
    private Sprite backSprite;
    private Animator animator;



    public SecondBossScript(GameObject bossbullet, GameObject beebullet, GameObject cloud)
    {
        //get necessary components
        this.eye = GameObject.Find("BossEye");
        this.bullet = bossbullet;
        this.beeBullet = beebullet;
        this.cloud = cloud;
        this.Boss = GameObject.Find("Boss2");
        

        this.front = GameObject.Find("Normal");
        this.animator = front.GetComponent<Animator>();


        this.back = GameObject.Find("Mad");
        backSr = back.GetComponent<SpriteRenderer>();
        backSprite = backSr.sprite;




        this.rb = Boss.GetComponent<Rigidbody2D>();
        frontSr = front.GetComponent<SpriteRenderer>();
        bossSprite = frontSr.sprite;
        bossColor = frontSr.color;

        this.dmg = GameObject.Find("DmgSpriteLal");

        this.dmgSprite = dmg.GetComponent<SpriteRenderer>().sprite;

        front.SetActive(true);
        back.SetActive(false);
        this.turn = 0;




    }





  

    public void EyeAttack() //shoots projectile towards player's position that follows player (this comes from enemybullet script)
    {


        var bull = Instantiate(beeBullet, eye.transform.position, Quaternion.identity);
      
        bull.SetActive(true);
        






    }
    public bool BossMoveTo(float coordinatex, float coordinatey, float speed) //moves towards position and returns true if at position
    {
        if (rb.transform.position.x <= coordinatex + 0.2f)
        {

            return true;

        }
        else
        {
            point = new Vector3(coordinatex, coordinatey, 0);
            rb.velocity = -speed * (Vector3)(rb.transform.position - point);


            return false;
        }

    }

    public void LazerAttack() //Shoots Lazers in random directions (bullet mechanics from enemybullet script -> this bullet is bouncy so it bounces from walls x amount of times)
    {

        var bull = Instantiate(bullet, eye.transform.position, Quaternion.identity);
        
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Rigidbody2D bullRb = bull.GetComponent<Rigidbody2D>();
        bull.transform.right = direction;
        bull.SetActive(true);
        bullRb.velocity = (direction) * 5;


      
    }
  

    public bool Turn() //method for turning boss' side
    {
        if (turn == 0)
        {
            front.SetActive(false);
            back.SetActive(true);

            turn = 1;
            return true;
        }
        else if (turn == 1)
        {
            back.SetActive(false);
            front.SetActive(true);

            turn = 0;
            return false;

        }
        else
        {
            return false;
        }
    }

    public bool FadeAttack() //boss fades from screen
    {

    
        
        frontSr.color = Color.Lerp(bossColor, new Color(1,1,1,0), Mathf.Lerp(0, 1, value)); //changes boss' color to transparent
        if (value < 1f)
        {
            value += 0.003f;
            return false;

        }
        else
        {
            value = 0;
            Boss.SetActive(false);
            var bull = Instantiate(cloud, rb.transform.position, Quaternion.identity); //spawns cloud in its place when faded
            bull.SetActive(true);
            return true;

        }
    }

    public void Teleport() //teleports to a random position between predetermined coordinates
    {
        
        rb.transform.position = new Vector2(Random.Range(-3.5f, 7f), Random.Range(-4, 3.9f));
        Boss.SetActive(true);
        




    }
    public bool FadeBack() //fades back to life
    {
        frontSr.color = Color.Lerp(new Color(1,1,1,0), bossColor, value2); //changes color back to normal
        if(value2 < 1)
        {
            value2 += 0.003f;
            return true;
        }
        else
        {
            value2 = 0;
            return false;
        }
        
    }
    //changes boss' sprite 
    public bool Flicker(bool turned, bool flickStatus) //turned 0 for frontside, 1 for backside; flickerstatus false for not flickered yet, true for flickered
    {

        if (!turned)
        {
            if (!flickStatus)
            {
                animator.enabled = false;
                frontSr.sprite = dmgSprite;
                return true;
            }
            else if (flickStatus)
            {
                frontSr.sprite = bossSprite;
                animator.enabled = true;
                
                return false;
            }
            else
            {
                return false;
            }


        }
        else if (turned)
        {
            if (!flickStatus)
            {
                backSr.sprite = dmgSprite;
                return true;
            }
            else if (flickStatus)
            {
                backSr.sprite = backSprite;
                return false;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }


}
