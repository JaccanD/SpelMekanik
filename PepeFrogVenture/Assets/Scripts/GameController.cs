using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float Health = 10;
    public int Berries = 0;
    public GameObject CurrentRespawnPoint;
    public GameObject Player;
    public bool Tounge = true;
    public bool fire = false;

    public HealthBar healthBar;


    public void Awake()
    {
        EventSystem.Current.RegisterListener(typeof(PlayerHitEvent), TakeDamage);
        EventSystem.Current.RegisterListener(typeof(RespawnPointReachedEvent), SetRespawnPoint);
        EventSystem.Current.RegisterListener(typeof(PickupEvent), OnPickup);
        EventSystem.Current.RegisterListener(typeof(ToungeFlickEvent), OnFlick);
        EventSystem.Current.RegisterListener(typeof(ToungeDoneEvent), OnToungeDone);
        EventSystem.Current.RegisterListener(typeof(EnemyPushesPlayerBack), PushPlayerBack);
        EventSystem.Current.RegisterListener(typeof(PlayerDeathEvent), IsSceneTwo);
    }
    public bool CheckTounge()
    {
        return Tounge;
    }
    public void AddHealth(float healthIncrease)
    {
        Health += healthIncrease;
        if(Health > 10)
        {
            Health = 10;
        }
    }
    public void TakeDamage(Callback.Event eb)
    {
        PlayerHitEvent e = (PlayerHitEvent)eb;
        Health -= e.Damage;
        Debug.Log(Health);
        if (Health <= 0)
            PlayerDead();
        healthBar.SetHealth((int) Health);
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
    public void SetRespawnPoint(Callback.Event eb)
    {
        RespawnPointReachedEvent e = (RespawnPointReachedEvent)eb;
        CurrentRespawnPoint = e.RespawnPoint;
    }
    public void OnPickup(Callback.Event eb)
    {
        PickupEvent e = (PickupEvent)eb;
        if (e.Pickup.tag == "Fire")
        {
            fire = true;
        }
        if (e.Pickup.tag == "Flies")
        {
            AddHealth(2);
            healthBar.SetHealth((int)Health);
        }
        if (e.Pickup.tag == "Berry")
        {
            AddBerry();
        }
    }
    public void OnFlick(Callback.Event eb)
    {
        ToungeFlickEvent e = (ToungeFlickEvent)eb;
        Tounge = false;
    }
    public void OnToungeDone(Callback.Event eb)
    {
        ToungeDoneEvent e = (ToungeDoneEvent)eb;
        Tounge = true;
    }
    public void IsSceneTwo(Callback.Event eb)
    {
        if (SceneManager.GetActiveScene().name == "LvL2")
            {
                SceneManager.LoadScene("LvL2");
            }
    }
    public void PushPlayerBack(Callback.Event eb)
    {
        EnemyPushesPlayerBack e = (EnemyPushesPlayerBack)eb;
        Vector3 direction = (e.player.transform.position - e.enemyPosition).normalized;
        e.player.GetComponent<PlayerKontroller3D>().SetVelocity(direction * e.pushBackStrenght + (Vector3.up * e.heightPush));
    }
}
