using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerKontroller3D player;
    protected PlayerKontroller3D Player => player = player ?? (PlayerKontroller3D)owner;
    protected Vector3 Velocity { get { return Player.GetVelocity();} set { Player.SetVelocity(value); } }
    protected float SkinWidth { get { return Player.GetSkinWidth(); } }
    protected LayerMask CollisionMask { get { return Player.GetCollisionMask(); } }
    protected LayerMask HookMask { get { return Player.GetHookMask(); } }
    protected CapsuleCollider Coll { get { return Player.GetCollider(); } }
    protected Transform transform { get { return Player.GetTransform(); } }
    protected float GroundCheckDistance { get { return Player.GetGroundCheckDistance(); } }
    protected Vector3 Direction { get { return Player.GetDirection(); } }
    protected GameObject Camera { get { return Player.GetCamera(); } }
    protected float ToungeLength { get { return Player.GetToungeLength(); } }
    protected LayerMask PickUpMask { get { return Player.GetPickUpMask(); } }
    protected GameController Controller { get { return Player.GetGameController(); } }
    protected GameObject Fireball { get { return Player.GetFireball(); } }
    protected LayerMask TalkMask {  get { return Player.GetTalkMask(); } }
    protected float Gravity = 9.82f;
    [SerializeField] float StaticFriktionKoeficcent = 0.3f;
    [SerializeField] float DynamicFriktionKoeficcent = 0.15f;

    protected void MovePlayer()
    {
        Vector3 nextMove = CheckCollision(Velocity * Time.deltaTime);
        transform.position += nextMove;
    }
    protected bool GroundCheck()
    {
        Vector3 topPoint = transform.position + Coll.center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 botPoint = transform.position + Coll.center + Vector3.down * (Coll.height / 2 - Coll.radius);
        RaycastHit cast;
        return Physics.CapsuleCast(topPoint, botPoint, Coll.radius, Vector3.down, out cast, SkinWidth + GroundCheckDistance, CollisionMask, QueryTriggerInteraction.Ignore);
    }
    protected Vector3 CheckCollision(Vector3 startingVelocity) 
    {
        if (startingVelocity.magnitude < 0.001f)
        {
            return Vector3.zero;
        }
        Vector3 topPoint = transform.position + Coll.center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 botPoint = transform.position + Coll.center + Vector3.down * (Coll.height / 2 - Coll.radius);
        RaycastHit cast;
        float addedDistance = GetAddedDistance(startingVelocity);
        bool hit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, startingVelocity.normalized, out cast, startingVelocity.magnitude + addedDistance, CollisionMask, QueryTriggerInteraction.Ignore);
        if (hit)
        {
            bool SkinWidthHit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, -cast.normal, out RaycastHit SkinWidthCast, SkinWidth + startingVelocity.magnitude, CollisionMask, QueryTriggerInteraction.Ignore);
            Vector3 normalForce = PhysicsFunctions.NormalForce(Velocity, cast.normal);
            Velocity += normalForce;
            ApplyFriktion(normalForce);
            if (SkinWidthHit) transform.position += (Vector3)(-SkinWidthCast.normal) * (SkinWidthCast.distance - SkinWidth);
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
        Vector3 topPoint = transform.position + Coll.center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 botPoint = transform.position + Coll.center + Vector3.down * (Coll.height / 2 - Coll.radius);
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
        bool hookHit = Physics.SphereCast(transform.position, 0.3f, Camera.transform.rotation * new Vector3(0, 0, 1), out RaycastHit HookCast, ToungeLength, HookMask);
        bool pickUpHit = Physics.SphereCast(transform.position, 0.3f, Camera.transform.rotation * new Vector3(0, 0, 1), out RaycastHit PickUpCast, ToungeLength, PickUpMask);
        if (hookHit)
        {
            Player.SetHook(HookCast.transform);
            stateMachine.TransitionTo<PlayerSwingState>();
            return;
        }
        if (pickUpHit)
        {
            string tag = PickUpCast.transform.gameObject.tag;
            if (tag == "Fire")
            {
                Controller.fire = true;
            }
            if (tag == "Flies")
            {
                Controller.AddHealth(2);
            }
            if (tag == "Berry")
            {
                Controller.AddBerry();
            }
            Destroy(PickUpCast.transform.gameObject);
        }
    }
    protected void Talk()
    {
        bool talkHit = Physics.SphereCast(transform.position, 0.2f, Camera.transform.rotation * new Vector3(0, 0, 1), out RaycastHit TalkCast, 5, TalkMask);
        if (talkHit)
        {
            TalkCast.transform.gameObject.GetComponent<NPC>().Talk();
        }
    }
}
