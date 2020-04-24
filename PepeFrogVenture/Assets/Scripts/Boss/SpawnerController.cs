using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject FireFlyPrefab;
    [SerializeField] private int MaxChildren;
    private int CurrentChildren;

    [SerializeField] private List<GameObject> Spawners;
    private Dictionary<GameObject, bool> IsOccupied;

    [SerializeField] private float RespawnDelay;
    private float Timer = 0;

    private void Start()
    {
        IsOccupied = new Dictionary<GameObject, bool>();
        foreach(GameObject s in Spawners)
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
        if (MaxChildren == CurrentChildren)
            return;

        Timer += Time.deltaTime;
        if (Timer < RespawnDelay)
            return;
        int index = 0;
        do
        {
            index = Random.Range(0, Spawners.Count);

        } while (IsOccupied[Spawners[index]]);
        GameObject spawnTarget = Spawners[index];
            if (!IsOccupied[spawnTarget])
            {
                Vector3 spawnPoint = spawnTarget.transform.position + Vector3.up * 2;
                Vector3 offset = Random.insideUnitSphere;
                offset.x *= 3;
                offset.z *= 3;
                spawnPoint += offset;
                GameObject newFlies = Instantiate(FireFlyPrefab, spawnPoint, transform.rotation);
                newFlies.GetComponent<FireFlyOnDestroy>().Parent = spawnTarget;
                IsOccupied[spawnTarget] = true;
                CurrentChildren++;
                Timer = 0;
            }
    }
    public void OnLilyPadDestroyed(Callback.Event eb)
    {
        LilyPadDestroyedEvent e = (LilyPadDestroyedEvent)eb;

        Spawners.Remove(e.Pad);
        IsOccupied.Remove(e.Pad);

        Debug.Log(Spawners.Count);
        Debug.Log(IsOccupied.Count);
    }
    public void OnEatEvent(Callback.Event eb)
    {
        FireFlyDeathEvent e = (FireFlyDeathEvent)eb;
        IsOccupied[e.Parent] = false;
        CurrentChildren--;
    }
}
