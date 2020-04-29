using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Callback
{
    public class PlayerDeathEvent : Event
    {
        public GameObject PlayerGameObject;
        public AudioClip PlayerDeathSound;
        public ParticleSystem PlayerDeathParticles;
        
        public PlayerDeathEvent(GameObject player)
        {
            PlayerGameObject = player;
        }
    }
}
