using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Valter Falsterljung
public class BossBaseState : State
{
    protected Boss boss;
    protected Boss Boss => boss = boss ?? (Boss)owner;
    protected LayerMask CollisionMask { get { return Boss.GetCollisionMask(); } }
    //protected CapsuleCollider Collider { get { return Boss.GetCollider(); } }
    //protected BoxCollider Collider { get { return Boss.getCollider(); } }
    protected PlayerKontroller3D Player { get { return Boss.GetPlayer(); } }
    protected Vector3 Position { get { return Boss.transform.position; } set { Boss.transform.position = value; } }
    protected GameObject[] SuperJumpPoints { get { return Boss.GetSuperJumpPoints(); } }
    protected GameObject Projectile { get { return Boss.getProjectile(); } }

    protected void RotateTowardPlayer(Vector3 rotateTowards, float rotationSpeed)
    {
        Quaternion rotation = Quaternion.LookRotation((rotateTowards - Boss.transform.position).normalized);
        Boss.transform.rotation = Quaternion.Slerp(Boss.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
    protected void RegularShoot(float projectileStartingForce, float projectileDistanceMultiplier, float projectileDamage)
    {
        float distance = Vector3.Distance(Boss.transform.position, Player.transform.position);
        float force = projectileStartingForce + (distance * projectileDistanceMultiplier);

        GameObject newProjectile;

        newProjectile = Instantiate(Projectile, Boss.getShootPoint().transform.position, Boss.getShootPoint().transform.rotation);
        newProjectile.GetComponent<BossProjectile>().SetDamage(projectileDamage);

        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * force);

        EventSystem.Current.FireEvent(new BossShootingEvent());
    }

    protected void SpreadShoot(float projectileStartingForce, float projectileDistanceMultiplier, float projectileDamage, float shootSpread)
    {
        float distance = Vector3.Distance(Boss.transform.position, Player.transform.position);
        float force = projectileStartingForce + (distance * projectileDistanceMultiplier);

        GameObject newProjectile;
        float xRotation = Random.Range(-shootSpread, shootSpread);
        float yRotation = Random.Range(-shootSpread, shootSpread);
        float zRotation = Random.Range(-shootSpread, shootSpread);
        newProjectile = Instantiate(Projectile, Boss.getShootPoint().transform.position, Boss.getShootPoint().transform.rotation * Quaternion.Euler(xRotation, yRotation, zRotation));
        newProjectile.GetComponent<BossProjectile>().SetDamage(projectileDamage);

        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * force);

        EventSystem.Current.FireEvent(new BossShootingEvent());
    }
}
