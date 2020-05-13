using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "BossState/BossReturnToStartPositionState")]
public class BossReturnToStartPositionState : BossBaseState
{
    private Vector3 startPosition;
    private Rigidbody rb;
    //private float parabolaAnimation;
    //private Vector3 parabolaStartPosition;
    //private Vector3 parabolaEndPosition;
    //private bool IsDone;
    //parabola saken från förut
    public override void Enter()
    {
        //parabolaStartPosition = Position;
        //parabolaEndPosition = Boss.GetStartPosition();
        //Debug.Log("divover");
        //IsDone = false;
        rb = Boss.GetComponent<Rigidbody>();
        Boss.GetComponent<Rigidbody>().AddForce(Boss.transform.forward *-50, ForceMode.Impulse);
    }
    public override void Run()
    {
        
        if(Position.y < startPosition.y -5)
        {
            Position = Boss.GetStartPosition() + Vector3.down * 5;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
            stateMachine.TransitionTo<BossDivingState>();
        }

        //if (!IsDone)
        //{
        //    parabolaAnimation += Time.deltaTime;

        //    parabolaAnimation = parabolaAnimation %= 5f;

        //    Position = Parabola(parabolaStartPosition, parabolaEndPosition, 5, parabolaAnimation/ 5);
        //}
        //if(Vector3.Distance(Position, parabolaEndPosition) < 0.2f)
        //{
        //    Position = parabolaEndPosition;
        //    Debug.Log("IIII");
        //    IsDone = true;
        //    stateMachine.TransitionTo<BossDivingState>();
        //}
    }
    //public Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    //{
    //    Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

    //    var mid = Vector3.Lerp(start, end, t);

    //    return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    //}

}
