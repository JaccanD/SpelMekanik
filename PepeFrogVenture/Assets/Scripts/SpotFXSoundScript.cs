using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotFXSoundScript : MonoBehaviour
{
    [SerializeField] private float MinDelay;
    [SerializeField] private float MaxDelay;
    [SerializeField] private AudioClip SpotFX;

    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = SpotFX;
    }
    void Update()
    {
        if (!source.isPlaying)
        {
            float d = Random.Range(MinDelay, MaxDelay);

            source.PlayDelayed(d);
        }
    }
}
