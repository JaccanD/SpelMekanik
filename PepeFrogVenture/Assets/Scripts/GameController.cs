using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class GameController : MonoBehaviour
{
    public float Health = 10;
    public int Berries = 0;
    public GameObject CurrentRespawnPoint;
    public GameObject Player;

    public bool fire = false;

    public void Awake()
    {
        EventSystem.Current.RegisterListener<PlayerHitEvent>(TakeDamage);
        

    }
    public void AddHealth(float healthIncrease)
    {
        Health += healthIncrease;
    }
    public void TakeDamage(PlayerHitEvent e)
    {
        Health -= e.Damage;
        Debug.Log(Health);
        if (Health <= 0)
            PlayerDead();
    }
    public void Update()
    {
        if(Health <= 0)
        {
            PlayerDead();
            Health = 10;
        }
    }
    public void AddBerry()
    {
        Berries++;
    }
    public void RemoveBerries()
    {
        Berries = 0;
    }
    private void PlayerDead()
    {
        PlayerDeathEvent e = new PlayerDeathEvent(Player);
        EventSystem.Current.FireEvent(e);
     }
}
