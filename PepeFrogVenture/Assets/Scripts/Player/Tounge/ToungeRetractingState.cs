using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "ToungeState/RetractingState")]

// Author: Jacob Didenbäck
public class ToungeRetractingState : ToungeBaseState
{
    private GameObject HitObject;

    public override void Enter()
    {
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position + Coll.center, Coll.radius / 4, ToungeMask);
        if(hits.Length > 0)
            HitObject = hits[0].gameObject;
        else
        {
            HitObject = null;
        }
    }
    public override void Run()
    {
        Vector3 start = gameObject.transform.position;
        Vector3 end = Player.transform.position;

        Vector3 toungeDirection = (start - end).normalized;
        Vector3 rotation = toungeDirection + Vector3.up;
        rotation = rotation.normalized;

        Quaternion rotate = new Quaternion(rotation.x, rotation.y, rotation.z, 0);

        Vector3 move = rotate * (-Speed * Vector3.up) * Time.deltaTime;
        gameObject.transform.position += move;
        if(HitObject !=  null)
            HitObject.transform.position += move;

        Destroy(Cylinder);
        drawTounge();
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position + Coll.center, Coll.radius / 4);
        for(int i = 0; i < hits.Length; i++)
        {
            if(hits[i].gameObject.tag == "Player")
            {
                EventSystem.Current.FireEvent(new ToungeDoneEvent());
                if (HitObject != null)
                {
                    EventSystem.Current.FireEvent(new PickupEvent(HitObject));
                    //Så vi kan återanvända flugor
                    HitObject.SetActive(false);
                    //Destroy(HitObject);
                }
                Destroy(Cylinder);
                Destroy(Tounge.gameObject);
            }
        }
    }
}
