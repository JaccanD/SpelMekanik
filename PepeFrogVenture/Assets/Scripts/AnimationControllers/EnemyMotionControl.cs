using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class EnemyMotionControl : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        EventSystem.Current.RegisterListener(typeof(EnemyAttackingEvent), EnemyAttack);
        EventSystem.Current.RegisterListener(typeof(EnemyDeathEvent), EnemyDeath);
        EventSystem.Current.RegisterListener(typeof(EnemyStompedEvent), EnemyStomped);
    }

    public void EnemyAttack(Callback.Event eb)
    {
        anim.SetTrigger("Attack");
    }
    
    public void EnemyDeath(Callback.Event eb)
    {
        EnemyDeathEvent e = (EnemyDeathEvent)eb;
        if(e.enemy.gameObject == transform.parent.gameObject)
        {
            anim.SetTrigger("Death");
            EventSystem.Current.UnRegisterListener(typeof(EnemyAttackingEvent), EnemyAttack);
            EventSystem.Current.UnRegisterListener(typeof(EnemyDeathEvent), EnemyDeath);
            EventSystem.Current.UnRegisterListener(typeof(EnemyStompedEvent), EnemyStomped);
        }
    }

    public void EnemyStomped(Callback.Event eb)
    {
        EnemyStompedEvent e = (EnemyStompedEvent)eb;
        if (e.enemyStomped == transform.parent.gameObject)
        {
            anim.SetTrigger("Death");
            EventSystem.Current.UnRegisterListener(typeof(EnemyAttackingEvent), EnemyAttack);
            EventSystem.Current.UnRegisterListener(typeof(EnemyDeathEvent), EnemyDeath);
            EventSystem.Current.UnRegisterListener(typeof(EnemyStompedEvent), EnemyStomped);
        }
    }
}
