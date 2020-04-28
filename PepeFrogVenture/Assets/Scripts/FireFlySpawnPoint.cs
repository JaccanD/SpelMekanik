using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlySpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    private GameObject currentFirefly;

    [SerializeField] private float spawnTimer = 5f;
    private float currentSpawnTimeLeft = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFirefly == null)
        {
            currentSpawnTimeLeft += Time.deltaTime;
            if(currentSpawnTimeLeft > spawnTimer)
            {
                Vector3 randomPos = Random.insideUnitSphere;
                randomPos *= 2;
                randomPos.y = 0;
                randomPos += transform.position;
                currentFirefly = Instantiate(Prefab, randomPos, Quaternion.identity);
                currentSpawnTimeLeft = 0;
            }
        }
    }
}
