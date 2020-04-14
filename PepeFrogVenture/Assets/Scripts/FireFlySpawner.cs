using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlySpawner : MonoBehaviour
{
    [SerializeField] private GameObject FireFly;
    [SerializeField] private float SpawnDelay;
    [SerializeField] private int MaxNumberOfFireFlies;
    [SerializeField] private float Radius;

    private float TimeSinceLastSpawn;
    private int CurrentNumberOfFireFlies;
    private GameObject[] SpawnedFlies;

    private void Update()
    {
        if (CurrentNumberOfFireFlies < MaxNumberOfFireFlies && TimeSinceLastSpawn >= SpawnDelay)
        {
            Vector3 position = new Vector3(1, 0, 1);
            Vector2 randomPos = Random.insideUnitCircle;
            position.x *= randomPos.x * Radius;
            position.z *= randomPos.y * Radius;

            position += transform.position;

            GameObject newFireFly = Instantiate(FireFly, position, transform.rotation);
            SpawnedFlies[CurrentNumberOfFireFlies] = newFireFly;

            CurrentNumberOfFireFlies++;
            TimeSinceLastSpawn = 0;
            Debug.Log(CurrentNumberOfFireFlies);
        }
        TimeSinceLastSpawn += Time.deltaTime;


        int test = 0;
        for(int i = 0; i < SpawnedFlies.Length; i++)
        {

            int FireFlies = 0;
            if(SpawnedFlies[i] != null)
            {
                FireFlies++;
                test++;
            }
            if(FireFlies == MaxNumberOfFireFlies)
            {
                TimeSinceLastSpawn = 0;
            }
        }
        CurrentNumberOfFireFlies = test;

        for(int i = 0; i < SpawnedFlies.Length - 1; i++)
        {
            if(SpawnedFlies[i] == null)
            {
                SpawnedFlies[i] = SpawnedFlies[i + 1];
                SpawnedFlies[i + 1] = null;
            }
        }
        if(SpawnedFlies[MaxNumberOfFireFlies - 1])
        {
            TimeSinceLastSpawn = 0;
        }
    }
    
    private void Awake()
    {
        SpawnedFlies = new GameObject[MaxNumberOfFireFlies];
    }

}
