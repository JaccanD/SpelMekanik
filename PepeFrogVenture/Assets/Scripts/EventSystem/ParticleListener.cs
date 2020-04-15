using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Callback
{
    public class ParticleListener : MonoBehaviour
    {
        void Start()
        {
            EventSystem.Current.RegisterListener<UnitDeathEvent>(OnUnitDied);

        }

        //void OnUnitSpawned(Health health)
        //{
        //    health.OnDeathListeners += OnUnitDied;
        //}
        // Update is called once per frame
        void Update()
        {

        }

        void OnUnitDied(UnitDeathEvent e)
        {
            if(e.UnitDeathParticles == null)
            {
                return;
            }
            e.UnitDeathParticles.Play();
        }
    }
}
