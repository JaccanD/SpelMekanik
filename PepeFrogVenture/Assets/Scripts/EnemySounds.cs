using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class EnemySounds : MonoBehaviour
{
    // hej proggare. Denna klass har Jag(Jack) gjort så om något är tokigt, feel free att göra vad ni vill med koden nedan!

    [SerializeField] private AudioClip EnemyDead;


    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Current.RegisterListener(typeof(EnemyHitEvent), OnEnemyHit);
    }

    public void OnEnemyHit(Callback.Event eb) 
    {

        //source.volume = 0.2f;
        //source.PlayOneShot(EnemyDead);

        AudioSource.PlayClipAtPoint(EnemyDead, transform.position);
        EventSystem.Current.UnRegisterListener(typeof(EnemyHitEvent), OnEnemyHit);
    }
}