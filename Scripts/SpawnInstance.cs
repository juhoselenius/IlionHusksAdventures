using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class SpawnInstance is used to create info of the spawns of the enemies (and textBoxActivators) for EnemySpawnManager.
/// </summary>

[System.Serializable]
public class SpawnInstance
{
    public int enemy;
    public float time;
    public int spawnpoint;
}
