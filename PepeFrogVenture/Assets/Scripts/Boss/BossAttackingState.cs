using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossState/AttackingState")]
public class BossAttackingState : BossBaseState
{
    protected GameObject projectile { get { return Boss.getProjectile(); } }
    private float currentCool;
    [SerializeField] private float cooldown;
    [SerializeField] private int shootsLeftBeforeSubmerge = 3;
    [SerializeField] private float rotationSpeed = 3;
    [SerializeField] private float projectileStartingForce = 1000;
    [SerializeField] private float projectileDamage = 4;
    [SerializeField] private float projectileDistanceMultiplier = 40;
    

    public override void Run()
    {
        rotateTowardPlayer(Boss.player.transform.position);
        Attack();
    }

    private void Attack()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;
        //gamla sättet att skuta projektiler
        //LaunchProjectile();
        //GameObject projectile = projectile;
        //projectile.GetComponent<BossProjectile>().setTarget(Boss.player.transform.position); gammalt
        ShootProjectile();

        shootsLeftBeforeSubmerge -= 1;
        if(shootsLeftBeforeSubmerge < 1)
        {
            shootsLeftBeforeSubmerge = 3;
            stateMachine.TransitionTo<BossDivingState>();
        }
        currentCool = cooldown;
    }
    private void rotateTowardPlayer(Vector3 rotateTowards)
    {
        Quaternion rotation = Quaternion.LookRotation((rotateTowards - Boss.transform.position).normalized);
        Boss.transform.rotation = Quaternion.Slerp(Boss.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
    private void ShootProjectile()
    {
        float distance = Vector3.Distance(Boss.transform.position, player.transform.position);
        float force = projectileStartingForce + (distance * projectileDistanceMultiplier);

        GameObject newProjectile;

        newProjectile = Instantiate(projectile, Boss.getShootPoint().transform.position, Boss.getShootPoint().transform.rotation);
        newProjectile.GetComponent<BossProjectile>().SetDamage(projectileDamage);

        Debug.Log(force);
        
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * force);
        
    }
    //private void LaunchProjectile()
    //{
    //    Vector3 velocity = CalculateVelocity(Boss.player.transform.position, Boss.getShootPoint().transform.position, 1f);
    //    Boss.transform.rotation = Quaternion.LookRotation(velocity);

    //    Rigidbody obj = Instantiate(Boss.getProjectile(), Boss.getShootPoint().transform.position, Quaternion.identity);
    //    obj.velocity = velocity;
    //}

    //private Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    //{
    //    Vector3 distance = target - origin;
    //    Vector3 distanceXZ = distance;
    //    distanceXZ.y = 0f;

    //    float Sy = distance.y;
    //    float Sxz = distanceXZ.magnitude;

    //    float Vxz = Sxz / time;
    //    float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

    //    Vector3 result = distanceXZ.normalized;
    //    result *= Vxz;
    //    result.y = Vy;

    //    return result;
    //}
}
