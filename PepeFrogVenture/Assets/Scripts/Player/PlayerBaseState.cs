using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
using System.Linq;

// Main Author: Jacob Didenbäck
// Secondary Author: Valter Falsterljung
public abstract class PlayerBaseState : State
{
    protected PlayerKontroller3D player;
    protected PlayerKontroller3D Player => player = player ?? (PlayerKontroller3D)owner;
    protected Vector3 Velocity { get { return Player.GetVelocity();} set { Player.SetVelocity(value); } }
    protected float SkinWidth { get { return Player.GetSkinWidth(); } }
    protected LayerMask CollisionMask { get { return Player.GetCollisionMask(); } }
    protected LayerMask HookMask { get { return Player.GetHookMask(); } }
    protected CapsuleCollider Coll { get { return Player.GetCollider(); } }
    protected Transform Transform { get { return Player.GetTransform(); } }
    protected float GroundCheckDistance { get { return Player.GetGroundCheckDistance(); } }
    protected Vector3 Direction { get { return Player.GetDirection(); } }
    protected GameObject Camera { get { return Player.GetCamera(); } }
    protected float ToungeLength { get { return Player.GetToungeLength(); } }
    protected LayerMask PickUpMask { get { return Player.GetPickUpMask(); } }
    protected GameController Controller { get { return Player.GetGameController(); } }
    protected GameObject Fireball { get { return Player.GetFireball(); } }
    protected LayerMask TalkMask {  get { return Player.GetTalkMask(); } }
    protected GameObject ToungePrefab { get { return Player.GetToungePrefab(); } }
    protected Vector3 Center { get { return Coll.center + Transform.position; } }
    [SerializeField] protected float Gravity = 9.82f;
    [SerializeField] float StaticFriktionKoeficcent = 0.3f;
    [SerializeField] float DynamicFriktionKoeficcent = 0.15f;

    
    //[SerializeField] string[] PickupTags = { "Berry", "Flies", "Fire" };

    protected void MovePlayer()
    {
        Vector3 nextMove = CheckCollision(Velocity * Time.deltaTime);
        Transform.position += nextMove;
    }
    protected bool GroundCheck()
    {
        Vector3 topPoint = Center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 botPoint = Center + Vector3.down * (Coll.height / 2 - Coll.radius);
        RaycastHit cast;
        return Physics.CapsuleCast(topPoint, botPoint, Coll.radius, Vector3.down, out cast, SkinWidth + GroundCheckDistance, CollisionMask, QueryTriggerInteraction.Ignore);
    }
    protected Vector3 CheckCollision(Vector3 startingVelocity) 
    {
        if (startingVelocity.magnitude < 0.001f)
        {
            return Vector3.zero;
        }
        Vector3 topPoint = Center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 botPoint = Center + Vector3.down * (Coll.height / 2 - Coll.radius);
        RaycastHit cast;
        float addedDistance = GetAddedDistance(startingVelocity);
        bool hit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, startingVelocity.normalized, out cast, startingVelocity.magnitude + addedDistance, CollisionMask, QueryTriggerInteraction.Ignore);
        
        if (hit)
        {
            bool SkinWidthHit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, -cast.normal, out RaycastHit SkinWidthCast, SkinWidth + startingVelocity.magnitude, CollisionMask, QueryTriggerInteraction.Ignore);
            Vector3 normalForce = PhysicsFunctions.NormalForce(Velocity, cast.normal);
            Velocity += normalForce;
            ApplyFriktion(normalForce);
            if (SkinWidthHit) Transform.position += (Vector3)(-SkinWidthCast.normal) * (SkinWidthCast.distance - SkinWidth);
            return CheckCollision(Velocity * Time.deltaTime);
        }
        else
        { 
            return startingVelocity;
        }
    }
    protected void ApplyFriktion(Vector3 normalForce)
    {
        if (normalForce.magnitude * StaticFriktionKoeficcent > Velocity.magnitude)
            Velocity = Vector3.zero;
        else
        {
            Velocity += normalForce.magnitude * DynamicFriktionKoeficcent * -Velocity.normalized;
        }
    }
    protected void ApplyAirResistance(float AR)
    {
        Velocity *= Mathf.Pow(AR, Time.deltaTime);
    }
    protected float GetAddedDistance(Vector3 startingVelocity)
    {
        Vector3 topPoint = Center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 botPoint = Center + Vector3.down * (Coll.height / 2 - Coll.radius);
        bool addedDistanceCastHit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, startingVelocity.normalized, out RaycastHit addedDistanceCast, float.MaxValue, CollisionMask);
        bool groundCastHit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, Vector3.down, out RaycastHit groundCast, float.MaxValue, CollisionMask);
        float dot = 0;
        float addedDistance = SkinWidth;

        if (addedDistanceCastHit)
        {
            dot = Vector3.Dot(startingVelocity.normalized, -addedDistanceCast.normal);
        }
         
        if (dot != 0)
        {
            addedDistance = SkinWidth / dot;
        }
        return addedDistance;
    }
    protected void ToungeFlick()
    {
        if (!Controller.CheckTounge())
        {
            return;
        }
        //Skicka event att vi använder tungan.
        //Spawna tungan i munnen
        //Tungan sträcker ut sig tills den träffar något
        Vector3 start = Center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 forward = Camera.transform.rotation * Vector3.forward;
        bool hookHit = Physics.SphereCast(Camera.transform.position, 0.3f, Camera.transform.rotation * new Vector3(0, 0, 1), out RaycastHit HookCast, ToungeLength, HookMask);
        if (!hookHit || HookCast.distance < (Camera.transform.position - Center).magnitude)
        {
            return;
        }
        EventSystem.Current.FireEvent(new ToungeFlickEvent());
        Vector3 end = HookCast.point;
        Vector3 toungeDirection = (end - start).normalized;
        Vector3 rotation = toungeDirection + Vector3.up;
        Quaternion rotate = new Quaternion(rotation.x, rotation.y, rotation.z, 0);
        GameObject go = Instantiate(ToungePrefab, Center + Vector3.up * (Coll.height / 2 - Coll.radius), rotate);
        go.GetComponent<Tounge>().SetPoint(end);

    }
    protected void Talk()
    {
        Collider[] colliders = Physics.OverlapSphere(Transform.position, 2f, TalkMask);
        foreach(Collider coll in colliders)
        {
            coll.gameObject.GetComponent<NPC>().Talk();
        }
    }
}
