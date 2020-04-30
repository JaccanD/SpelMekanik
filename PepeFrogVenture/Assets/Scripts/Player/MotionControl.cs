using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class MotionControl : MonoBehaviour
{
    private Animator anim;
    private float speed;
    private float direction;
    private State CurrentState { get { return GetComponent<PlayerKontroller3D>().InState(); } }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            EventSystem.Current.FireEvent(new PlayerDabbing(transform.position));
            anim.SetTrigger("Dab");
        }
    
        if (CurrentState.GetType() == typeof(PlayerMovingState) || CurrentState.GetType() == typeof(PlayerStandingState))
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
        if (Input.GetKeyDown("space"))
            anim.SetTrigger("Jump");
    }
}
