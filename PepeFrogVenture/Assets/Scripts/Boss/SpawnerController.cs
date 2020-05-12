using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
// Author: Jacob Didenbäck
public class SpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject fireFlyPrefab;
    [SerializeField] private int maxChildren;
    [SerializeField] private float respawnDelay;
    [SerializeField] private List<GameObject> spawners;

    private int currentChildren;
    private Dictionary<GameObject, bool> IsOccupied;
    private float timer = 0;

    private void Start()
    {
        IsOccupied = new Dictionary<GameObject, bool>();
        foreach(GameObject s in spawners)
        {
            IsOccupied[s] = false;
        }

        //Registrera Lyssnare för när en EldFluga Äts Upp
        System.Type test = typeof(FireFlyDeathEvent);

        EventSystem.Current.RegisterListener(test, OnEatEvent);
        EventSystem.Current.RegisterListener(typeof(LilyPadDestroyedEvent), OnLilyPadDestroyed);
    }
    private void Update()
    {
        if (maxChildren == currentChildren)
            return;

        timer += Time.deltaTime;
        if (timer < respawnDelay)
            return;
        int index = 0;
        do
        {
            index = Random.Range(0, spawners.Count);

        } while (IsOccupied[spawners[index]]);
        GameObject spawnTarget = spawners[index];
            if (!IsOccupied[spawnTarget])
            {
                Vector3 spawnPoint = spawnTarget.transform.position + Vector3.up * 2;
                Vector3 offset = Random.insideUnitSphere;
                offset.x *= 3;
                offset.z *= 3;
                spawnPoint += offset;
                GameObject newFlies = Instantiate(fireFlyPrefab, spawnPoint, transform.rotation);
                newFlies.GetComponent<FireFlyOnDestroy>().Parent = spawnTarget;
                IsOccupied[spawnTarget] = true;
                currentChildren++;
                timer = 0;
            }
    }
    public void OnLilyPadDestroyed(Callback.Event eb)
    {
        LilyPadDestroyedEvent e = (LilyPadDestroyedEvent)eb;

        spawners.Remove(e.Pad);
        IsOccupied.Remove(e.Pad);

        //Debug.Log(spawners.Count);
        //Debug.Log(IsOccupied.Count);
    }
    public void OnEatEvent(Callback.Event eb)
    {
        FireFlyDeathEvent e = (FireFlyDeathEvent)eb;
        IsOccupied[e.Parent] = false;
        currentChildren--;
    }
}
