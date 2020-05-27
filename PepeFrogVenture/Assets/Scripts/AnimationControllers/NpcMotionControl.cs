using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class NpcMotionControl : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        EventSystem.Current.RegisterListener(typeof(QuestDoneEvent), Thank);
    }

    public void Thank(Callback.Event eb)
    {
        anim.SetTrigger("Thank");
    }
}
