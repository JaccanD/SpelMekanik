using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
// Main Author: Jack Noaksson
// Secondary Author: Jacob Didenbäck
public class EnemySounds : MonoBehaviour
{
    // hej proggare. Denna klass har Jag(Jack) gjort så om något är tokigt, feel free att göra vad ni vill med koden nedan!

    [SerializeField] private AudioClip EnemyDead;
    [SerializeField] private AudioClip FlatRat;


    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Current.RegisterListener(typeof(EnemyHitEvent), OnEnemyHit);
        EventSystem.Current.RegisterListener(typeof(EnemyStompedEvent), OnEnemyStomp);
    }

    public void OnEnemyHit(Callback.Event eb) 
    {
        EnemyHitEvent e = (EnemyHitEvent)eb;
        if (e.EnemyHit != gameObject)
            return;

        //source.volume = 0.2f;
        //source.PlayOneShot(EnemyDead);

        AudioSource.PlayClipAtPoint(EnemyDead, transform.position);
        UnRegisterListeners();
    }

    public void OnEnemyStomp(Callback.Event eb)
    {
        EnemyStompedEvent e = (EnemyStompedEvent)eb;
        if (e.enemyStomped != gameObject)
            return;

        AudioSource.PlayClipAtPoint(FlatRat, transform.position);
        UnRegisterListeners();
    }
    private void UnRegisterListeners()
    {
        EventSystem.Current.UnRegisterListener(typeof(EnemyHitEvent), OnEnemyHit);
        EventSystem.Current.UnRegisterListener(typeof(EnemyStompedEvent), OnEnemyStomp);
    }
}