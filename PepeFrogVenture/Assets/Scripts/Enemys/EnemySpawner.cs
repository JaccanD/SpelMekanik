using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject[] patrolPoints;
    private GameObject spawnedObject;

    void Start()
    {
        EventSystem.Current.RegisterListener(typeof(PlayerDeathEvent), RespawnEnemy);
        if(spawnedObject == null)
        {
            SpawnEnemy();
        }
    }
    private void RespawnEnemy(Callback.Event e)
    {
        if(spawnedObject == null)
        {
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().setPatrolPoints(patrolPoints);
        spawnedObject = newEnemy;
    }
}
