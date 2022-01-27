using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//class that contains enemy spawn info
public class TimeScript
{
    public string name;
    public GameObject enemy;
    public List<float> spawnTimes = new List<float>();
    public List<int> spawnPointId = new List<int>();
}
