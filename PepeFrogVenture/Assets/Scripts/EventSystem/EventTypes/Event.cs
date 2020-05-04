using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Jacob Didenbäck
// Alla Event types är gjora av Jacob
namespace Callback
{
    public abstract class Event
    {
    }
    public class DebugEvent : Event
    {
        public string DebugText;

        public DebugEvent(string debugText)
        {
            DebugText = debugText;
        }
    }
    public class UnitDeathEvent : Event
    {
        public GameObject UnitGO;
        public ParticleSystem UnitDeathParticles;
        public AudioClip UnitDeathSound;
        public UnitDeathEvent(GameObject gameObject, ParticleSystem unitDeathParticles, AudioClip unitDeathSound)
        {
            UnitGO = gameObject;
            UnitDeathParticles = unitDeathParticles;
            UnitDeathSound = unitDeathSound;
        }
        public UnitDeathEvent(GameObject gameObject)
        {
            UnitGO = gameObject;
        }
    }
}
