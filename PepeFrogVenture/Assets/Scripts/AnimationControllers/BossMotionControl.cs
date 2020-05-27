using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Main Author: August Brunsätter
public class BossMotionControl: MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        EventSystem.Current.RegisterListener(typeof(BossDivingState), BossDive);
    }

    void Update()
    {

    }

    public void BossDive(Callback.Event eb)
    {
        anim.SetTrigger("Dive");
    }

}
