using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class EnemySpawnManager manages the spawns of the enemies in one level.
/// </summary>

public class EnemySpawnManager : MonoBehaviour
{
    public float timer = 0;
    private int instanceCounter = 0;
    public List<SpawnInstance> spawns = new List<SpawnInstance>();
    public List<Transform> transforms = new List<Transform>();
    public List<GameObject> spawningEnemies = new List<GameObject>();
    private List<SpawnInstance> sortedSpawns = new List<SpawnInstance>();
    public bool spawnTextBoxWhenBossDestroyed = false;
    public float timeToActivateAfterBoss = 0;

    void Start()
    {
        sortedSpawns = spawns.OrderBy(o => o.time).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(instanceCounter < spawns.Count)
        {
            if (timer >= sortedSpawns[instanceCounter].time)
            {
                int enemyType = sortedSpawns[instanceCounter].enemy;
                Spawn(spawningEnemies[enemyType], sortedSpawns[instanceCounter].spawnpoint);
                instanceCounter++;
            }
        }
    }

    public void Spawn(GameObject gameObject, int i)
    {
        Instantiate(gameObject, transforms[i].position, Quaternion.identity);
    }

    public void SpawnTextBoxActivator()
    {

        bool activatorInList = false;
        int activatorIndex = -1;
        //Checks if activator is in the list.
        for (int i = 0; i < spawningEnemies.Count; i++)
        {
            if(spawningEnemies[i].name.Equals("TextBoxActivator"))
            {
                activatorInList = true;
                activatorIndex = i;
            }
        }

        if(activatorInList)
        {
            if(transforms.Count != 0)
            {
                Debug.Log("Got this far");
                StartCoroutine(Delay(activatorIndex));
                Debug.Log("TextBoxActivator created!");
            }
        }
    }

    //Coroutine that delays the text box activator after boss's death.
    IEnumerator Delay(int activatorIndex)
    {
        yield return new WaitForSeconds(timeToActivateAfterBoss);
        Instantiate(spawningEnemies[activatorIndex], transforms[0].position, Quaternion.identity);
    }
}
