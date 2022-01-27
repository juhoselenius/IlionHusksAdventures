using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//class that contains power up info
public class PowerUp 
{
    public string name;
    public string description;
    public int AddedHealth;
    public int AddedArmor;
    [Range(0, 20)]
    public int TemporarySpeed;
    [Range(1f, 0.01f)]
    public float TemporaryFireRate;
    public float powerUpTime;
    public bool invulnerability;
}
