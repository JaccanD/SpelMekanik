using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Callback
{

    public class DeathListener : MonoBehaviour
    {
        // Start is called before the first frame update
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
            Destroy(e.UnitGO);
        }
    }
}
