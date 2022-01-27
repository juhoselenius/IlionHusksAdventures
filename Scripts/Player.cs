using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //early stage playermanager that makes the player take damage
    public int Health;
    public int Armor;
    public int Speed;
    public float FireRate;

    PlayerController controller;
    void Start()
    {
        controller = GetComponent<PlayerController>();
        controller.speed = Speed;
        controller.fireRate = FireRate;
    }
    
    public void TakeDamage(int damage) {
        if(Armor <= 0) {
            if(Health - damage <= 0) {
                Debug.Log("Dead");
            }
            else {
                Health -= damage;
            }
        }
        else {
            if(Armor - damage < 0) {
                Armor = 0;
            }
            else {
                Armor -= damage;
            }
        }
    }

    public void AddHealth(int hp) {
        Health += hp;
    }

    
   
}
