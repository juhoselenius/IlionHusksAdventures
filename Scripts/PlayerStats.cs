using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//class that contains player info
public class PlayerStats 
{    
    public int Level;
    public int Health;
    public int Armor;
    [Range(0, 20)]
    public int Speed;
    [Range(1f, 0.01f)]
    public float FireRate;

}
