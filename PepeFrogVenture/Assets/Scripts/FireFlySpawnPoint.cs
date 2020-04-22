using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlySpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject fireFlyPreFab;
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
                currentFirefly = Instantiate(fireFlyPreFab, transform.position, Quaternion.identity);
                currentSpawnTimeLeft = 0;
            }
        }
    }
}
