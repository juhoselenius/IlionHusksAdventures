using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leveltester : MonoBehaviour
{
    //this script is for developer testing
    // Start is called before the first frame update
    GameObject gameManager;
    void Start()
    {
        gameManager = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            gameManager.GetComponent<PlayerManager>().TakeDamage(10);
        } else if (Input.GetKeyDown(KeyCode.K)) {
            gameManager.GetComponent<PlayerManager>().AddScore(100);
        } else if (Input.GetKeyDown(KeyCode.J)) {
            var enemy = GameObject.FindGameObjectsWithTag("Enemy");
            int rand = Random.Range(0, enemy.Length);
            enemy[rand].GetComponent<AEnemy>().TakeDamage(1000);
        }
    }
}
