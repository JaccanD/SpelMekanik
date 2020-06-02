using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Valter Fallsterljung
public class BossBaseState : State
{
    protected Boss boss;
    protected Boss Boss => boss = boss ?? (Boss)owner;
    protected LayerMask CollisionMask { get { return Boss.GetCollisionMask(); } }
    protected PlayerControl Player { get { return Boss.GetPlayer(); } }
    protected Vector3 Position { get { return Boss.transform.position; } set { Boss.transform.position = value; } }
    protected Vector3 StartPosition { get { return Boss.GetStartPosition(); } }
    protected List<DestroyableLilypad> Lilypads { get { return Boss.GetLilyPads(); } }
    protected GameObject[] SuperJumpPoints { get { return Boss.GetSuperJumpPoints(); } }
    protected GameObject Projectile { get { return Boss.GetProjectile(); } }
    protected GameObject ShootPoint { get { return Boss.GetShootPoint(); } }
    protected float ProjectileDamage { get { return Boss.GetProjectileDamage(); } }
    protected float ProjectileStartingForce { get { return Boss.GetProjectileStartingForce(); } }
    protected float ProjectileDistanceForceMultiplier { get { return Boss.GetProjectileDistanceForceMultiplier(); } }
    protected float ChargeAttackDamage { get { return Boss.GetChargeAttackDamage(); } }
    protected float SuperAttackHealthThreshold { get { return Boss.GetSuperAttackHealthThreshold(); } }

    protected void RotateTowardPlayer(Vector3 rotateTowards, float rotationSpeed)
    {
        Quaternion rotation = Quaternion.LookRotation((rotateTowards - Boss.transform.position).normalized);
        Boss.transform.rotation = Quaternion.Slerp(Boss.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    protected void Shoot(float shootSpread)
    {
        //Calculate the initial velocity of the projectile based on how far away the player is
        float distance = Vector3.Distance(Boss.transform.position, Player.transform.position);
        float force = ProjectileStartingForce + (distance * ProjectileDistanceForceMultiplier);

        Quaternion randomSpread = Quaternion.Euler(Random.Range(-shootSpread, shootSpread), Random.Range(-shootSpread, shootSpread), Random.Range(-shootSpread, shootSpread));
        GameObject newProjectile = ObjectPooler.instance.GetPooledObject(Projectile.tag);
        newProjectile.transform.position = ShootPoint.transform.position;
        newProjectile.transform.rotation = ShootPoint.transform.rotation * randomSpread;

        newProjectile.GetComponent<BossProjectile>().SetDamage(ProjectileDamage);
        newProjectile.SetActive(true);
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * force);
    }
}
