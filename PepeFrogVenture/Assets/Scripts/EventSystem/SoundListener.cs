using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Callback
{
    public class SoundListener : MonoBehaviour
    {
        [SerializeField] private AudioSource Audio;
        private Dictionary<AudioClip, int> PlayedSounds = new Dictionary<AudioClip, int>();

        void Start()
        {
            EventSystem.Current.RegisterListener<UnitDeathEvent>(OnUnitDied);
        }
        void Update()
        {

            Dictionary<AudioClip, int> PlayedSoundsCopy = new Dictionary<AudioClip, int>(PlayedSounds);
            Dictionary<AudioClip, int>.KeyCollection keyColl = PlayedSoundsCopy.Keys;
            foreach(KeyValuePair<AudioClip,int> entry in PlayedSoundsCopy)
            {
                if(PlayedSounds[entry.Key] > 0)
                    PlayedSounds[entry.Key]--;
            }
        }
        void OnUnitDied(UnitDeathEvent e)
        {
            if (!PlayedSounds.ContainsKey(e.UnitDeathSound))
            {
                PlayedSounds.Add(e.UnitDeathSound, 0);
            }
            PlayedSounds[e.UnitDeathSound]++;
            if (e.UnitDeathSound != null && PlayedSounds[e.UnitDeathSound] < 5)
            {
                Audio.PlayOneShot(e.UnitDeathSound);
            }
        }
    }
}