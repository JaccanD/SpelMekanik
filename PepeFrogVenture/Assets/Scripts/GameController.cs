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
    public float SceneTwoRespawnTime = 1.3f;
    [SerializeField] private bool RestartWholeLevelOnDeath;

    public void Start()
    {
        PlayerStats.setFire(false);
        EventSystem.Current.RegisterListener(typeof(PlayerHitEvent), TakeDamage);
        EventSystem.Current.RegisterListener(typeof(RespawnPointReachedEvent), SetRespawnPoint);
        EventSystem.Current.RegisterListener(typeof(PickupEvent), OnPickup);
        EventSystem.Current.RegisterListener(typeof(ToungeFlickEvent), OnFlick);
        EventSystem.Current.RegisterListener(typeof(ToungeDoneEvent), OnToungeDone);
        EventSystem.Current.RegisterListener(typeof(EnemyPushesPlayerBack), PushPlayerBack);
        EventSystem.Current.RegisterListener(typeof(PlayerDabbing), OnDab);
        EventSystem.Current.RegisterListener(typeof(PlayerDeathEvent), RestartScene);
        //EventSystem.Current.RegisterListener(typeof(PlayerDeathEvent), IsSceneTwo);
        EventSystem.Current.RegisterListener(typeof(BossDeadEvent), BossIsDead);
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
            PlayerStats.setFire(true);
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

            StartCoroutine(WaitForSceneLoad("ExtraScene", 1.3f));

        }
    }
    public void OnToungeDone(Callback.Event eb)
    {
        ToungeDoneEvent e = (ToungeDoneEvent)eb;
        Tounge = true;
    }
    IEnumerator WaitForSceneLoad(string scene, float time)
    {
        yield return new WaitForSeconds(1.3f);
        
        SceneManager.LoadScene(scene);
    }
    public void RestartScene(Callback.Event eb)
    {
        if (RestartWholeLevelOnDeath)
        {
            Debug.Log("respawning");
            PlayerStats.setHealth(10);
            PlayerStats.setFire(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
            
    }
    //public void IsSceneTwo(Callback.Event eb)
    //{
    //    PlayerStats.setHealth(10);
    //    PlayerStats.setFire(false);
    //    if (SceneManager.GetActiveScene().name == "LvL2")
    //        {
    //            StartCoroutine(WaitForSceneLoad("LvL2", SceneTwoRespawnTime));
    //        }
    //}
    public void BossIsDead(Callback.Event e)
    {
        SceneManager.LoadScene("Lvl3Slut");
    }
    public void PushPlayerBack(Callback.Event eb)
    {
        EnemyPushesPlayerBack e = (EnemyPushesPlayerBack)eb;
        Vector3 direction = (e.player.transform.position - e.enemyPosition).normalized;
        e.player.GetComponent<PlayerKontroller3D>().SetVelocity(direction * e.pushBackStrenght + (Vector3.up * e.heightPush));
    }
}
