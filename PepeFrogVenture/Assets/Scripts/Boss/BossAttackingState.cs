using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossState/AttackingState")]
public class BossAttackingState : BossBaseState
{
    private GameObject projectile;
    [SerializeField] private float cooldown;
    private float currentCool;
    [SerializeField] private int shootsLeftBeforeSubmerge = 3;
    

    public override void Run()
    {
        float rotationSpeed = Boss.speed * Time.deltaTime;
        Boss.transform.rotation = Quaternion.LookRotation(Boss.player.transform.position);
        //Boss.transform.position = Vector3.RotateTowards(Boss.transform.position, Boss.player.transform.position, rotationSpeed,0);
        Attack();
    }

    private void Attack()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;

        //Debug.Log("Boss attacking");

        //LaunchProjectile();
        GameObject projectile = Boss.getProjectile();
        projectile.GetComponent<BossProjectile>().setTarget(Boss.player.transform.position);
        Instantiate(projectile, Boss.getShootPoint().transform.position, Quaternion.identity);

        shootsLeftBeforeSubmerge -= 1;
        if(shootsLeftBeforeSubmerge < 1)
        {
            shootsLeftBeforeSubmerge = 3;
            stateMachine.TransitionTo<BossDivingState>();
        }
        currentCool = cooldown;
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
