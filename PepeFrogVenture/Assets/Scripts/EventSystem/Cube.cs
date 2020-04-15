using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private float Delay;
        [SerializeField] private ParticleSystem DeathParticles;
        [SerializeField] private AudioClip DeathSound;
        private bool Inflating = false;
        private float InflationSpeed = 1.0f;
        // Start is called before the first frame update
        void Awake()
        {
            DeathParticles = Instantiate(DeathParticles, transform.position, new Quaternion(0, 0, 0, 0));
        }
        private void OnMouseDown()
        {
            Inflating = true;
            Invoke("Die", Delay);
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Die();
            }
            if (Inflating)
            {
                Inflate();
            }
        }
        void Inflate()
        {
            transform.localScale += new Vector3(1, 1, 1) * InflationSpeed * Time.deltaTime;
        }
        void Die()
        {
            UnitDeathEvent udei = new UnitDeathEvent(gameObject, DeathParticles, DeathSound);
            EventSystem.Current.FireEvent(new DebugEvent(gameObject.name + " just died!"));
            EventSystem.Current.FireEvent(udei);
        }
    }
}
