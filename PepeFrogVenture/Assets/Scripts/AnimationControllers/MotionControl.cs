using System.Collections;
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
        EventSystem.Current.RegisterListener(typeof(PlayerJumpEvent), Jump);
        EventSystem.Current.RegisterListener(typeof(FireballshotEvent), Spit);
        EventSystem.Current.RegisterListener(typeof(HookHitEvent), Swing);
        EventSystem.Current.RegisterListener(typeof(PlayerHitEvent), WaterBounce);
        EventSystem.Current.RegisterListener(typeof(PlayerLandingEvent), Land);
        EventSystem.Current.RegisterListener(typeof(QuestDoneEvent), Give);
    }

    // Update is called once per frame
    void Update()
    {

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

    }

    public void Jump(Callback.Event eb)
    {
        anim.SetTrigger("Jump");
        anim.ResetTrigger("Land");
        anim.ResetTrigger("Swing");
    }


    public void Spit(Callback.Event eb)
    {
        anim.SetTrigger("Spit");
    }

    public void Swing(Callback.Event eb)
    {
        anim.SetTrigger("Swing");
        anim.ResetTrigger("Land");
        anim.ResetTrigger("Jump");
    }

    public void WaterBounce(Callback.Event eb)
    {
        PlayerHitEvent e = (PlayerHitEvent) eb;
        if (e.enemyHit == true){
            return;
        }
        anim.SetTrigger("WaterBounce");
    }

    public void Land(Callback.Event eb)
    {
        anim.SetTrigger("Land");
        anim.ResetTrigger("Jump");
        anim.ResetTrigger("Swing");
    }

    public void Give(Callback.Event eb)
    {
        anim.SetTrigger("Give");
    }
}
