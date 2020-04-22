﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "ToungeState/ExtendingState")]
public class ToungeExtendingState : ToungeBaseState
{
    [SerializeField] private LayerMask HookMask;
    [SerializeField] private LayerMask PickUpMask;
    public enum HIT_TYPE {
        PICKUP_HIT,
        HOOK_HIT,
        NO_HIT
   };
    public override void Run()
    {
        HIT_TYPE check = CheckHit();
        if(check == HIT_TYPE.PICKUP_HIT)
        {
            Debug.Log("tror den träffar en pickup");
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
            Debug.Log("Den kommer till Max range");
            stateMachine.TransitionTo<ToungeRetractingState>();
            return;
        }
        Vector3 move = gameObject.transform.localRotation * (Speed * Vector3.up) * Time.deltaTime;
        gameObject.transform.position += move;

        Destroy(Cylinder);
        drawTounge();
    }

    private HIT_TYPE CheckHit()
    {
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position + Coll.center, Coll.radius / 4, ToungeMask);
        if (hits.Length > 0)
        {
            Debug.Log(hits[0]);
            Debug.Log(hits[0].gameObject.name);
        }
        for (int i = 0; i < hits.Length; i++)
        {
            int layerValue =(int) Mathf.Pow(2.0f, (float)(hits[i].gameObject.layer));
            Debug.Log(layerValue + "::" + HookMask.value);
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