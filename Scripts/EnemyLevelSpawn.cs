using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelSpawn : MonoBehaviour {
    public float timer = 0;
    public List<TimeScript> timeScripts = new List<TimeScript>();
    public List<Transform> transforms = new List<Transform>();

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        foreach(TimeScript script in timeScripts) {
            Spawner(script);
        }
    }
    
    private void Spawner(TimeScript script) {
        List<float> spawntimes = script.spawnTimes;
        List<int> spawnPoints = script.spawnPointId;
        if(spawntimes.Count > 0) {
            for(int i = 0; i < spawntimes.Count; i++) {
                //Debug.Log("Spawntimes " + script.name + " index: " + i);
                if (timer >= spawntimes[i]) {
                    Debug.Log("Spawntimes" + script.name + "count: " + spawntimes.Count);
                    int id = spawnPoints[i];
                    spawntimes.RemoveAt(i);
                    spawnPoints.RemoveAt(i);
                    Debug.Log("Spawntimes" + script.name + "count: " + spawntimes.Count);
                    Spawn(script.enemy, script.spawnPointId[id]);
                }
            }
        }
    }

    public void Spawn(GameObject gameObject, int i) {
        Instantiate(gameObject, transforms[i].position, Quaternion.identity);
    }
}
