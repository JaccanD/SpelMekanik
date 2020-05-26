﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Main Author: August Brunsätter
// Secondary Author: Jacob Didenbäck
public class MotionControl : MonoBehaviour
{
    private Animator anim;
    private float speed;
    private float direction;
    private State CurrentState { get { return controller.InState; } }

    private PlayerControl controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerControl>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Controlls.GetKeyBinding(Function.Interact)))
        {
            EventSystem.Current.FireEvent(new PlayerDabbing(transform.position));
            anim.SetTrigger("Dab");
        }
    
        if (CurrentState.GetType() == typeof(PlayerControlMovementState) || CurrentState.GetType() == typeof(PlayerControlIdleState))
        {
            speed = Input.GetAxis("Vertical");
            direction = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", speed);
            anim.SetFloat("Direction", direction);
        }
        else
        {
            speed = 0;
            direction = 0;
            anim.SetFloat("Speed", speed);
            anim.SetFloat("Direction", direction);
        }
        if (Input.GetKeyDown(Controlls.GetKeyBinding(Function.Jump)))
            anim.SetTrigger("Jump");
    }
}
