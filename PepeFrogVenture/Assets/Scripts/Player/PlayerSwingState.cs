using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "PlayerState/SwingState")]
// Author: Jacob Didenbäck
public class PlayerSwingState : PlayerBaseState
{
    private Vector3 Hook { get { return Player.GetHook(); } }
    private GameObject Tounge;
    private float CurrentToungeLength;
    [SerializeField] private float ToungeForce;

    private float Acceleration = 3;

    public override void Enter()
    {
        drawTounge();
        Vector3 dir = (Hook - transform.position).normalized;
        Vector3 reset = Velocity;
        reset *= 0;
        Velocity = reset;
        Velocity += ToungeForce * dir;

        stateMachine.TransitionTo<PlayerJumpingState>();
    }
    public override void Exit()
    {
        Destroy(Tounge);
    }
    private void drawTounge()
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Vector3 start = transform.position;
        Vector3 end = Hook;
        Vector3 toungePos = (start + end) / 2.0f;

        Vector3 toungeDirection = (end - start).normalized;
        Vector3 rotation = toungeDirection + Vector3.up;
        rotation = rotation.normalized;

        Quaternion rotate = new Quaternion(rotation.x, rotation.y, rotation.z, 0);

        cylinder.transform.position = toungePos;
        cylinder.transform.rotation = rotate;
        cylinder.transform.localScale = new Vector3(0.3f, (end - start).magnitude / 2, 0.3f);

        CurrentToungeLength = (end - start).magnitude;
        Tounge = cylinder;
    }
}
