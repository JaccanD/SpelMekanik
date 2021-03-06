﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "ToungeState/ExtendingState")]

// Author: Jacob Didenbäck
public class ToungeExtendingState : ToungeBaseState
{
    [SerializeField] private LayerMask HookMask;
    [SerializeField] private LayerMask PickUpMask;

    private Vector3 target;
    public enum HIT_TYPE {
        PICKUP_HIT,
        HOOK_HIT,
        NO_HIT
   };
    public override void Enter()
    {
        target = Tounge.GetPoint();
    }
    public override void Run()
    {
        HIT_TYPE check = CheckHit();
        if(check == HIT_TYPE.PICKUP_HIT)
        {
            stateMachine.TransitionTo<ToungeRetractingState>();
            return;
        }
        if(check == HIT_TYPE.HOOK_HIT)
        {
            //Event för att en hook har träffats
            //Lyssnare i player som byter state till swing state?

            stateMachine.TransitionTo<ToungePullState>();
            return;
            //Ett state för tungan?
        }
        if((Player.transform.position - gameObject.transform.position).magnitude >= MaxDistance)
        {
            stateMachine.TransitionTo<ToungeRetractingState>();
            return;
        }
        if (target == gameObject.transform.position)
        {
            Collider[] hits = Physics.OverlapSphere(gameObject.transform.position + Coll.center, Coll.radius, ToungeMask);
            if (hits.Length > 0)
            {
                target = hits[0].gameObject.transform.position;
            }
            else
            {
                stateMachine.TransitionTo<ToungeRetractingState>();
                return;
            }
        }
        Vector3 directionToHook = target - gameObject.transform.position;
        float distance = directionToHook.magnitude;
        directionToHook = directionToHook.normalized;

        Vector3 move = (Speed * directionToHook) * Time.deltaTime;
        if (move.magnitude < distance)
            gameObject.transform.position += move;
        else
            gameObject.transform.position = target;

        Destroy(Cylinder);
        drawTounge();


    }
    private HIT_TYPE CheckHit()
    {
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position + Coll.center, Coll.radius / 4, ToungeMask);
        for (int i = 0; i < hits.Length; i++)
        {
            int layerValue =(int) Mathf.Pow(2.0f, (float)(hits[i].gameObject.layer));
            if (layerValue == HookMask.value || layerValue == PickUpMask.value)
            {
                if (layerValue == HookMask)
                {
                    return HIT_TYPE.HOOK_HIT;
                }
                if (layerValue == PickUpMask)
                {
                    return HIT_TYPE.PICKUP_HIT;
                }
            }
        }

        return HIT_TYPE.NO_HIT;
    }
}
