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
    protected GameObject[] SuperJumpPoints { get { return Boss.GetSuperJumpPoints(); } }
    protected GameObject Projectile { get { return Boss.GetProjectile(); } }
    protected GameObject ShootPoint { get { return Boss.GetShootPoint(); } }
    protected float projectileDamage { get { return Boss.GetProjectileDamage(); } }
    protected float projectileStartingForce { get { return Boss.GetProjectileStartingForce(); } }
    protected float projectileDistanceForceMultiplier { get { return Boss.GetProjectileDistanceForceMultiplier(); } }

    protected void RotateTowardPlayer(Vector3 rotateTowards, float rotationSpeed)
    {
        Quaternion rotation = Quaternion.LookRotation((rotateTowards - Boss.transform.position).normalized);
        Boss.transform.rotation = Quaternion.Slerp(Boss.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
    //protected void RegularShoot(float projectileStartingForce, float projectileDistanceMultiplier, float projectileDamage)
    //{
    //    float distance = Vector3.Distance(Boss.transform.position, Player.transform.position);
    //    float force = projectileStartingForce + (distance * projectileDistanceMultiplier);

    //    GameObject newProjectile;

    //    newProjectile = Instantiate(Projectile, Boss.getShootPoint().transform.position, Boss.getShootPoint().transform.rotation);
    //    newProjectile.GetComponent<BossProjectile>().SetDamage(projectileDamage);

    //    newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * force);

    //    EventSystem.Current.FireEvent(new BossShootingEvent());
    //}

    protected void Shoot(float projectileStartingForce, float projectileDistanceMultiplier, float projectileDamage, float shootSpread)
    {
        float distance = Vector3.Distance(Boss.transform.position, Player.transform.position);
        float force = projectileStartingForce + (distance * projectileDistanceMultiplier);

        //GameObject newProjectile = projectilePool.GetObjectInstance();
        GameObject newProjectile = ObjectPooler.instance.GetPooledObject(Projectile.tag);
        Quaternion randomSpread = Quaternion.Euler(Random.Range(-shootSpread, shootSpread), Random.Range(-shootSpread, shootSpread), Random.Range(-shootSpread, shootSpread));
        newProjectile.transform.position = ShootPoint.transform.position/*ShootPoint.transform.position*/;
        newProjectile.transform.rotation = ShootPoint.transform.rotation * randomSpread/*ShootPoint.transform.rotation * randomSpread*/;
        newProjectile.GetComponent<BossProjectile>().SetDamage(projectileDamage);
        newProjectile.SetActive(true);
        //GameObject newProjectile;
        ////float xRotation = Random.Range(-shootSpread, shootSpread);
        ////float yRotation = Random.Range(-shootSpread, shootSpread);
        ////float zRotation = Random.Range(-shootSpread, shootSpread);
        //Quaternion randomSpread = Quaternion.Euler(Random.Range(-shootSpread, shootSpread), Random.Range(-shootSpread, shootSpread), Random.Range(-shootSpread, shootSpread));
        //newProjectile = Instantiate(Projectile, Boss.GetShootPoint().transform.position, Boss.GetShootPoint().transform.rotation * randomSpread/*Quaternion.Euler(xRotation, yRotation, zRotation)*/);
        //newProjectile.GetComponent<BossProjectile>().SetDamage(projectileDamage);

        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * force);

        //EventSystem.Current.FireEvent(new BossShootingEvent());
    }
}
