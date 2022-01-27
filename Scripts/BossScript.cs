using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*This script contains mechanics for boss 1 that can be used 
 * in ABoss script. This script contains all methods for level 1 boss' attacs and movement
*/

public class BossScript : MonoBehaviour
{
    private Rigidbody2D rbBullet;
    private GameObject bullet;
    private List<Vector2> directionList; //list of unit vectors for cone attack
    private GameObject eye1;
    private List<Color> colorList; //list of colors to be used in buttAttack
    private GameObject eye2;
    private GameObject mouth;
    private GameObject butt;
    private GameObject cone;
    private GameObject Boss;
    private GameObject front;
    private GameObject back;
    private Rigidbody2D rb;
    private Vector3 point;
    private int eyenum; 
    private int turn; //0 for front, 1 for back
    private GameObject sinbullet;
    private SpriteRenderer sr;
    private int colorchanger = 0;
    private float value = 0;
    private Sprite dmgSprite;
    private Sprite dmgSpriteB;
    private Sprite sprite;
    private Sprite spriteB;
    private SpriteRenderer frontSr;
    private SpriteRenderer backSr;
    private GameObject dmg;
    private GameObject dmgB;



    public BossScript(GameObject bossbullet, GameObject conebullet, GameObject sinbullet)
    {
        //get necessary components & initialize local variables
        this.eye1 = GameObject.Find("Eye1");
        this.eye2 = GameObject.Find("Eye2");
        this.mouth = GameObject.Find("Mouth");
        this.butt = GameObject.Find("Butt");
        this.cone = GameObject.Find("BossConeBullet");
        this.Boss = GameObject.Find("Boss1");
        this.front = GameObject.Find("Front");
        front.SetActive(true);

        this.back = GameObject.Find("Back");
        

        this.sinbullet = sinbullet;

        this.turn = 0;

        this.rb = Boss.GetComponent<Rigidbody2D>();
        this.eyenum = 1;

        this.bullet = bossbullet;
        this.cone = conebullet;
        directionList = new List<Vector2>(); //adds vectors for cone attack in list
        directionList.Add(new Vector2(1, 0));
        directionList.Add(new Vector2( 0.9659f , 0.2588f)); // 15 degree angle
        directionList.Add(new Vector2(0.9659f, -0.2588f));
        directionList.Add(new Vector2((Mathf.Sqrt(3) / 2), 0.5f)); //30 degree angle
        directionList.Add(new Vector2((Mathf.Sqrt(3) / 2), -0.5f));
        directionList.Add(new Vector2((Mathf.Sqrt(2) / 2),  (Mathf.Sqrt(2) / 2))); //45 degree angle
        directionList.Add(new Vector2((Mathf.Sqrt(2) / 2), -(Mathf.Sqrt(2) / 2)));
        directionList.Add(new Vector2(0.5f , (Mathf.Sqrt(3) / 2)));//60 degree angle
        directionList.Add(new Vector2(0.5f, -(Mathf.Sqrt(3) / 2)));


        colorList = new List<Color>(); //adds colors for butAttac in a list
        colorList.Add(Color.red);
        colorList.Add(Color.cyan);
        colorList.Add(Color.yellow);
        colorList.Add(Color.green);
        colorList.Add(Color.blue);
        colorList.Add(Color.grey);

        //get the sprite for flickering
        this.dmg = GameObject.Find("DmgSprite");
        this.dmgB = GameObject.Find("DmgSpriteBack");
         
        this.dmgSprite = dmg.GetComponent<SpriteRenderer>().sprite;
        
        this.dmgSpriteB = dmgB.GetComponent<SpriteRenderer>().sprite;

        this.frontSr = front.GetComponent<SpriteRenderer>();
        this.sprite = frontSr.sprite;

        this.backSr = back.GetComponent<SpriteRenderer>();
        this.spriteB = backSr.sprite;
        back.SetActive(false);

    }

   

    

    public void ConeAttack() //instantiates bullets in a cone, alternates between eyes
    {
       
          
        if(eyenum == 1)
        {
            foreach (Vector2 i in directionList) //instantiates bullets in all directions in list containing vectors
            {
                Vector2 direction = i;
                var bull = Instantiate(cone, eye1.transform.position, Quaternion.identity);
                bull.transform.right = direction;
                bull.SetActive(true);
                Rigidbody2D bullRb = bull.GetComponent<Rigidbody2D>();
                bullRb.AddForce((Vector2)(direction) * -300);
                
            }
            eyenum = 2;

        }
        else if(eyenum == 2)
        {
            foreach (Vector2 i in directionList)
            {
                Vector2 direction = i;
                var bull = Instantiate(cone, eye2.transform.position, Quaternion.identity);
                bull.transform.right = direction;
                bull.SetActive(true);
                Rigidbody2D bullRb = bull.GetComponent<Rigidbody2D>();
                bullRb.AddForce((Vector2)(direction) * -300);

            }
            eyenum = 1;
        }
           
            

        
       
    }

    public void MouthAttack() //shoots projectile towards player's position ^^give grow status for this bullet^^ 
    {

        GameObject player = GameObject.Find("PlayerPos");
        Vector2 direction = (Vector2)(mouth.transform.position - player.transform.position); //get normalized vector that points towards player
        direction.Normalize();
        var bull = Instantiate(bullet, mouth.transform.position, Quaternion.identity);
        bull.transform.right = direction;
        bull.SetActive(true);
        Rigidbody2D bullRb = bull.GetComponent<Rigidbody2D>();
        bullRb.AddForce((Vector2)(direction) * -100);



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

    public void UpDownMovement(bool delay, float speed) //moves up and down randomly
    {

        //go down if going too far up
        if (rb.transform.position.y > 2.8f) 
        {
            rb.velocity = new Vector2(0, Random.Range(0.2f, 1f)) * -speed;
        }
        //go up if going too far down
        else if (rb.transform.position.y < -2.8) 
        {
            rb.velocity = new Vector2(0, Random.Range(0.2f, 1f)) * speed;
        }
        //change direction randomly after delay
        else if (delay) 
        {
            int i = Random.Range(0, 2);
            if(i == 0)
            {
                rb.velocity = new Vector2(0, Random.Range(0.2f, 1f)) * speed;
            }
            else if(i == 1)
            {
                rb.velocity = new Vector2(0, Random.Range(0.2f, 1f)) * -speed;
            }
            

            
        }
    }
    public void ButtAttack()   //attacks from butt and linearly changes colors between colors in colorlist (this bullet has sin mechanics from enemybullet script so it moves like a wave function y = sin(time)
    {


        var bull = Instantiate(sinbullet, butt.transform.position, Quaternion.identity);
        sr = bull.GetComponentInChildren<SpriteRenderer>(); //get srpiterenderer to change colors for

        if (colorchanger >= colorList.Count - 1) //if list is gone through reverse it and start again
        {
            colorList.Reverse();
            colorchanger = 0;
            value = 0;
        }
        //linearly changes between 2 colors
        sr.color = Color.Lerp(colorList[colorchanger], colorList[colorchanger + 1], Mathf.Lerp(0, 1, value));// https://docs.unity3d.com/ScriptReference/Color.Lerp.html, https://docs.unity3d.com/ScriptReference/Mathf.Lerp.html
        value += 0.005f;       // chage this value for faster/slower changespeed                             
        if (value >= 1) //change colors
        {

            colorchanger++; //changes to next color in colorlist
            value = 0;
        }
        
        

        bull.SetActive(true);
       
    }

    public bool Turn() //method for turning boss' side
    {
        if(turn == 0)
        {
            front.SetActive(false);
            back.SetActive(true);
            
            turn = 1;
            return true;
        }
        else if(turn == 1)
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

    //"flickers" eg. changes boss' sprite
    public bool Flicker(bool turned, bool flickStatus) //turned 0 for frontside, 1 for backside; flickerstatus false for not flickered yet, true for flickered
    {
        if(!turned)
        {
            if (!flickStatus)
            {
                frontSr.sprite = dmgSprite;
                return true;
            }
            else if (flickStatus)
            {
                frontSr.sprite = sprite;
                return false;
            }
            else
            {
                return false;
            }
            

        }
        else if(turned)
        {
            if (!flickStatus)
            {
                backSr.sprite = dmgSpriteB;
                return true;
            }
            else if (flickStatus)
            {
                backSr.sprite = spriteB;
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
