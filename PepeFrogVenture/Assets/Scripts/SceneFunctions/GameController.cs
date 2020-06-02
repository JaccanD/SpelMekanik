using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
using UnityEngine.SceneManagement;

// Main Author: Jacob Didenbäck
// Secondary Author: Valter Falsterljung
public class GameController : MonoBehaviour
{
    public int Berries = 0;
    public GameObject CurrentRespawnPoint;
    public GameObject Player;
    public Vector3 secretDab;
    public bool Tounge = true;
    public bool HasFire;
    [SerializeField] private Animator fadeAnimation;
    [SerializeField] private bool restartWholeLevelOnDeath;
    [SerializeField] private float respawnTime;
    


    public void Start()
    {
        EventSystem.Current.RegisterListener(typeof(PlayerHitEvent), TakeDamage);
        EventSystem.Current.RegisterListener(typeof(RespawnPointReachedEvent), SetRespawnPoint);
        EventSystem.Current.RegisterListener(typeof(PickupEvent), OnPickup);
        EventSystem.Current.RegisterListener(typeof(ToungeFlickEvent), OnFlick);
        EventSystem.Current.RegisterListener(typeof(ToungeDoneEvent), OnToungeDone);
        EventSystem.Current.RegisterListener(typeof(PlayerDabbing), OnDab);
        EventSystem.Current.RegisterListener(typeof(PlayerDeathEvent), Respawn);
        EventSystem.Current.RegisterListener(typeof(BossDeadEvent), BossIsDead);
        EventSystem.Current.RegisterListener(typeof(BoosDyingEvent), BossDying);
        EventSystem.Current.RegisterListener(typeof(QuestDoneEvent), RemoveBerries);

    }
    public bool CheckTounge()
    {
        return Tounge;
    }
    public void AddHealth(float healthIncrease)
    {
        PlayerStats.changeHealth(healthIncrease);
        if(PlayerStats.getHealth() > 10)
        {
            PlayerStats.setHealth(10);
        }
    }
    public void TakeDamage(Callback.Event eb)
    {
        PlayerHitEvent e = (PlayerHitEvent)eb;
        PlayerStats.changeHealth(- e.Damage);
        if (PlayerStats.getHealth() <= 0)
            PlayerDead();
    }
    public void Update()
    {
        if(PlayerStats.getHealth() <= 0)
        {
            PlayerDead();
            PlayerStats.changeHealth(10);
        }
    }
    public void AddBerry()
    {
        Berries++;
    }
    public void RemoveBerries(Callback.Event eb)
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
            HasFire = true;
        }
        if (e.Pickup.tag == "Flies")
        {
            AddHealth(2);
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
    public void OnDab(Callback.Event eb)
    {
        PlayerDabbing e = (PlayerDabbing)eb;
        if (secretDab != null && Vector3.Distance(secretDab, e.dabLocation) < 3 && SceneManager.GetActiveScene().name == "LvL1terrain")
        {
            StartCoroutine(WaitForSceneLoad("ExtraScene"));
        }
    }
    public void OnToungeDone(Callback.Event eb)
    {
        ToungeDoneEvent e = (ToungeDoneEvent)eb;
        Tounge = true;
    }
    private void BossDying(Callback.Event eb)
    {
        fadeAnimation.SetTrigger("End");
    }
    IEnumerator WaitForSceneLoad(string scene)
    {
        yield return new WaitForSeconds(respawnTime);
        PlayerStats.setHealth(10);
        SceneManager.LoadScene(scene);
    }
    IEnumerator WaitForPlayerRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        PlayerStats.ResetHealth();
        Player.transform.position = CurrentRespawnPoint.transform.position;
        fadeAnimation.ResetTrigger("End");
        EventSystem.Current.FireEvent(new PlayerRespawnEvent(CurrentRespawnPoint));
    }
    public void Respawn(Callback.Event eb)
    {
        if (restartWholeLevelOnDeath)
        {
            fadeAnimation.SetTrigger("End");
            StartCoroutine(WaitForSceneLoad(SceneManager.GetActiveScene().name));
        }
        else
        {
            fadeAnimation.SetTrigger("End");
            StartCoroutine(WaitForPlayerRespawn());
        }
    }
    public void BossIsDead(Callback.Event e)
    {
        SceneManager.LoadScene("Lvl3Slut");
    }
}
