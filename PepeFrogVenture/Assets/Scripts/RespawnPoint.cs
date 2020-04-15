using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class RespawnPoint : MonoBehaviour
{
    public void Update()
    {
        bool hit = Physics.BoxCast(transform.position, transform.localScale, Vector3.up, out RaycastHit cast, transform.rotation, 1);
        if (hit)
        {
            if(cast.transform.gameObject.tag == "Player")
            {
                EventSystem.Current.FireEvent(new RespawnPointReachedEvent(gameObject));
            }
        }
    }
}
