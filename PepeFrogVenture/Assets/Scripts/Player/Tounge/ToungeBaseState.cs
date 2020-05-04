using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author: Jacob Didenbäck
public abstract class ToungeBaseState : State
{
    protected Tounge tounge;
    protected Tounge Tounge => tounge = tounge ?? (Tounge)owner;

    protected GameObject gameObject { get { return Tounge.GetGameObject(); } }
    protected float Speed { get { return Tounge.GetSpeed(); } }
    protected LayerMask ToungeMask { get { return Tounge.GetToungeMask(); } }
    protected GameObject Player { get { return Tounge.GetPlayer(); } }
    protected SphereCollider Coll { get { return Tounge.GetColl(); } }
    protected GameObject Cylinder { get { return Tounge.GetCylinder(); } set { Tounge.SetCylinder(value); } }
    protected Vector3 Point { get { return Tounge.GetPoint(); } }
    protected float MaxDistance { get { return Tounge.GetMaxDistance(); } }

    protected void drawTounge()
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Vector3 start = gameObject.transform.position;
        CapsuleCollider coll = Player.GetComponent<CapsuleCollider>();
        Vector3 end = Player.transform.position + Vector3.up * (coll.height / 2);
        Vector3 toungePos = (start + end) / 2.0f;

        Vector3 toungeDirection = (end - start).normalized;
        Vector3 rotation = toungeDirection + Vector3.up;
        rotation = rotation.normalized;

        Quaternion rotate = new Quaternion(rotation.x, rotation.y, rotation.z, 0);

        cylinder.transform.position = toungePos;
        cylinder.transform.rotation = rotate;
        cylinder.transform.localScale = new Vector3(0.1f, (end - start).magnitude / 2, 0.1f);

        Cylinder = cylinder;
    }

}
