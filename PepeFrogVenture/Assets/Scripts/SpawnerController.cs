using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject FireFlyPrefab;
    [SerializeField] private int MaxChildren;
    private int CurrentChildren;

    [SerializeField] private GameObject[] Spawners;
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
        EventSystem.Current.RegisterListener<FireFlyDeathEvent>(OnEatEvent);
    }
    private void Update()
    {
        if (MaxChildren == CurrentChildren)
            return;

        Timer += Time.deltaTime;
        if (Timer < RespawnDelay)
            return;

        foreach(GameObject s in Spawners)
        {
            if (!IsOccupied[s])
            {
                GameObject newFlies = Instantiate(FireFlyPrefab, s.transform.position + Vector3.up * 2, transform.rotation);
                newFlies.GetComponent<FireFlyOnDestroy>().Parent = s;
                IsOccupied[s] = true;
                CurrentChildren++;
                Timer = 0;
                break;
            }
        }
    }

    public void OnEatEvent(FireFlyDeathEvent e)
    {
        IsOccupied[e.Parent] = false;
        CurrentChildren--;
    }
}
