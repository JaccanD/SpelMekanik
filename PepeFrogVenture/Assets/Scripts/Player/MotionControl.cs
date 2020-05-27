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
    private bool falling;
    private float t;
    private State CurrentState { get { return controller.InState; } } 

    private PlayerControl controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerControl>();
        anim = GetComponent<Animator>();
        EventSystem.Current.RegisterListener(typeof(PlayerJumpEvent), Jump);
        EventSystem.Current.RegisterListener(typeof(PlayerFallingEvent), Fall);
        EventSystem.Current.RegisterListener(typeof(FireballshotEvent), Spit);
        EventSystem.Current.RegisterListener(typeof(HookHitEvent), Swing);
        EventSystem.Current.RegisterListener(typeof(PlayerHitEvent), WaterBounce);
        EventSystem.Current.RegisterListener(typeof(PlayerLandingEvent), Land);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(Controlls.GetKeyBinding(Function.Interact)))
        //{
        //    EventSystem.Current.FireEvent(new PlayerDabbing(transform.position));
        //    anim.SetTrigger("Dab");
        //}

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
            //if (Input.GetKeyDown(Controlls.GetKeyBinding(Function.Jump)))
            //anim.SetTrigger("Jump");

            // if (falling)
            //{
            //    float fallingValue = Mathf.Lerp(1, -1, t);
            //    t += Time.deltaTime * 5;
            //    anim.SetFloat("Jumping", fallingValue);
            //}
    }

    public void Jump(Callback.Event eb)
    {
        anim.SetTrigger("Jump");
        anim.ResetTrigger("Land");
    }

    public void Fall(Callback.Event eb)
    {
        //t = 0;
        //falling = true;
        anim.SetTrigger("Falling");
        anim.ResetTrigger("Jump");
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
    }

    public void Land(Callback.Event eb)
    {
        anim.SetTrigger("Land");
        anim.ResetTrigger("Jump");
        anim.ResetTrigger("Swing");
    }

    public void WaterBounce(Callback.Event eb)
    {
        PlayerHitEvent e = (PlayerHitEvent) eb;
        if (e.enemyHit == true){
            return;
        }
        anim.SetTrigger("WaterBounce");
    }
}
