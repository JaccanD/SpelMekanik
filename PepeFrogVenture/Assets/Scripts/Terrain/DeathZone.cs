using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class DeathZone : MonoBehaviour
{
    BoxCollider coll;
    [SerializeField]private ParticleSystem waterSplash;
    private float timer = 0;

    public void Awake()
    {
        coll = GetComponent<BoxCollider>();
    }
    public void Update()
    {

        Collider[] overLaps = Physics.OverlapBox(transform.position + coll.center, coll.bounds.extents, transform.rotation);
        for (int i = 0; i < overLaps.Length; i++)
        {
            if (overLaps[i].transform.gameObject.tag == "Player" && timer <= 0)
            {
                timer = 1;
                GameObject.Instantiate(waterSplash, overLaps[i].transform); 
                EventSystem.Current.FireEvent(new PlayerDeathEvent(overLaps[i].transform.gameObject));
                break;
            }
            timer -= Time.deltaTime;
        }
    }
}
