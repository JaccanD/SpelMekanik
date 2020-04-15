using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject PreFab;

        private float Radius = 10;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SpawnUnit();
            }
        }
        void SpawnUnit()
        {
            Vector3 randomPos = Random.insideUnitSphere * Radius;
            GameObject go = Instantiate(PreFab, randomPos, new Quaternion(0,0,0,0));
        }
    }
}