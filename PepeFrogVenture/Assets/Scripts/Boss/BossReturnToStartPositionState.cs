using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "BossState/BossReturnToStartPositionState")]
public class BossReturnToStartPositionState : BossBaseState
{
    private float parabolaAnimation;
    private Vector3 parabolaStartPosition;
    private Vector3 parabolaEndPosition;
    //parabola saken från förut
    public override void Enter()
    {
        parabolaStartPosition = Position;
        parabolaEndPosition = Boss.GetStartPosition();
        Debug.Log("divover");
    }
    public override void Run()
    {
        parabolaAnimation += Time.deltaTime;

        parabolaAnimation = parabolaAnimation %= 5f;

        Position = Parabola(parabolaStartPosition, parabolaEndPosition, 5, parabolaAnimation/ 5);

        if(Vector3.Distance(Position, parabolaEndPosition) < 0.5f)
        {
            Position = parabolaEndPosition;
            Debug.Log("IIII");
            stateMachine.TransitionTo<BossDivingState>();
        }
    }
    public Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

}
