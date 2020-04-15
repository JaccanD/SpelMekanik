using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class RespawnPoint : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            EventSystem.Current.FireEvent(new RespawnPointReachedEvent(gameObject));
        }
    }
}
