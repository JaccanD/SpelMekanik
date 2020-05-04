using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "ToungeState/PullState")]

// Author: Jacob Didenbäck
public class ToungePullState : ToungeBaseState
{
    public override void Enter()
    {
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position + Coll.center, Coll.radius / 4);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.tag == "Hook")
            {
                EventSystem.Current.FireEvent(new HookHitEvent(hits[i].gameObject, Point));
            }
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

        Vector3 move = rotate * (-Speed * 3 * Vector3.up) * Time.deltaTime;
        gameObject.transform.position += move;

        Destroy(Cylinder);
        drawTounge();
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position + Coll.center, Coll.radius / 4);

        Vector3 between = Player.gameObject.transform.position - gameObject.transform.position;
        if(Vector3.Dot(between,move) > 0)
        {
            Destroy(Tounge.gameObject);
            Destroy(Cylinder);
            EventSystem.Current.FireEvent(new ToungeDoneEvent());
        }
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.tag == "Player")
            {
                Destroy(Tounge.gameObject);
                Destroy(Cylinder);
                EventSystem.Current.FireEvent(new ToungeDoneEvent());


            }
        }
    }
}
