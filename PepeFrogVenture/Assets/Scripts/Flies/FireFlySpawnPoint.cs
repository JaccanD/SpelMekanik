using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Valter Falsterljung
public class FireFlySpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    public GameObject currentFirefly;

    [SerializeField] private float spawnTimer = 5f;
    private float currentSpawnTimeLeft = 0;

    private void Start()
    {
        currentFirefly = ObjectPooler.instance.GetPooledObject(Prefab.tag);
        SpawnFly(GetSpawnPosition());
    }

    void Update()
    {
        //if(currentFirefly == null)
        if(!currentFirefly.activeInHierarchy)
        {
            currentSpawnTimeLeft += Time.deltaTime;
            if(currentSpawnTimeLeft > spawnTimer)
            {
                Vector3 randomPos = Random.insideUnitSphere;
                randomPos *= 2;
                randomPos.y = 0;
                randomPos += transform.position;
                //currentFirefly = Instantiate(Prefab, randomPos, Quaternion.identity);
                SpawnFly(randomPos);
                currentSpawnTimeLeft = 0;
            }
        }
    }
    private Vector3 GetSpawnPosition()
    {
        Vector3 randomPos = Random.insideUnitSphere;
        randomPos *= 2;
        randomPos.y = 0;
        randomPos += transform.position;
        return randomPos;
    }
    private void SpawnFly(Vector3 spawnPosition)
    {
        currentFirefly.transform.position = spawnPosition;
        currentFirefly.transform.rotation = transform.rotation;
        currentFirefly.SetActive(true);
    }
}
