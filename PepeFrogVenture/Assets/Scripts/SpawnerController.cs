using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private int MaxChildren;
    private int CurrentChildren;

    [SerializeField] private GameObject[] Spawners;
    private bool[] IsOccupied;



    [SerializeField] private float RespawnDelay;
    private float Timer = 0;

    private void Start()
    {
        IsOccupied = new bool[Spawners.Length];
        for(int i = 0; i < Spawners.Length; i++)
        {
            IsOccupied[i] = false;
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


    }

    public void OnEatEvent(FireFlyDeathEvent e)
    {
        //Få vilken parent flugan hade från eventet
        // Hitta parenten i Spawners
        // Sätt den platsen i IsOccupied till false;
    }
}
