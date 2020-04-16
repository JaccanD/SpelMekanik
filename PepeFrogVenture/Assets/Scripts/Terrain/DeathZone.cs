using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class DeathZone : MonoBehaviour
{
    BoxCollider coll;
    public void Awake()
    {
        coll = GetComponent<BoxCollider>();
    }
    public void Update()
    {

        Collider[] overLaps = Physics.OverlapBox(transform.position + coll.center, coll.bounds.extents, transform.rotation);
        for (int i = 0; i < overLaps.Length; i++)
        {
            if (overLaps[i].transform.gameObject.tag == "Player")
            {
                EventSystem.Current.FireEvent(new PlayerDeathEvent(overLaps[i].transform.gameObject));
                break;
            }
        }
    }
}
