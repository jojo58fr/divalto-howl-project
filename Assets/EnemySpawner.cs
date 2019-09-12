using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float maxSpawnTime = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

    private float currentTime;
    private float randMaxTime;

    private void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        randMaxTime = Random.Range(0f, maxSpawnTime);
    }

    private void Update()
    {
        if (GameController.instance.gameover || !GameController.instance.isStart)
        {
            // ... exit the function.
            return;
        }

        currentTime += Time.deltaTime;
        
        if(currentTime >= randMaxTime)
        {
            currentTime = 0;
            randMaxTime = Random.Range(0f, maxSpawnTime);

            Spawn();
        }
    }

    void Spawn()
    {
        // If the player has no health left...


        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
