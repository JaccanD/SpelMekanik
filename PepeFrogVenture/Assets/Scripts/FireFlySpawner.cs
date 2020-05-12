using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author: Jacob Didenbäck
// Används inte
public class FireFlySpawner : MonoBehaviour
{
    [SerializeField] private GameObject fireFly;
    [SerializeField] private float spawnDelay;
    [SerializeField] private int maxNumberOfFireFlies;
    [SerializeField] private float radius;

    private float timeSinceLastSpawn;
    private int currentNumberOfFireFlies;
    private GameObject[] spawnedFlies;

    private void Update()
    {
        if (currentNumberOfFireFlies < maxNumberOfFireFlies && timeSinceLastSpawn >= spawnDelay)
        {
            Vector3 position = new Vector3(1, 1, 1);
            Vector2 randomPos = Random.insideUnitCircle;
            position.x *= randomPos.x * radius;
            position.z *= randomPos.y * radius;

            position += transform.position;

            GameObject newFireFly = Instantiate(fireFly, position, transform.rotation);
            spawnedFlies[currentNumberOfFireFlies] = newFireFly;

            currentNumberOfFireFlies++;
            timeSinceLastSpawn = 0;
            //Debug.Log(CurrentNumberOfFireFlies);
        }
        timeSinceLastSpawn += Time.deltaTime;


        int test = 0;
        for(int i = 0; i < spawnedFlies.Length; i++)
        {

            int FireFlies = 0;
            if(spawnedFlies[i] != null)
            {
                FireFlies++;
                test++;
            }
            if(FireFlies == maxNumberOfFireFlies)
            {
                timeSinceLastSpawn = 0;
            }
        }
        currentNumberOfFireFlies = test;

        for(int i = 0; i < spawnedFlies.Length - 1; i++)
        {
            if(spawnedFlies[i] == null)
            {
                spawnedFlies[i] = spawnedFlies[i + 1];
                spawnedFlies[i + 1] = null;
            }
        }
        if(spawnedFlies[maxNumberOfFireFlies - 1])
        {
            timeSinceLastSpawn = 0;
        }
    }
    
    private void Awake()
    {
        spawnedFlies = new GameObject[maxNumberOfFireFlies];
    }

}
