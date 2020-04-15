using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class BossProjectile : MonoBehaviour
{
    private float speed;
    public Vector3 target;
    private Vector3 start;
    [SerializeField] private float damage = 4;

    private void Start()
    {
        start = transform.position;
        Destroy(gameObject, 5f);
    }

    public void setTarget(Vector3 targetVector)
    {
        target = targetVector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            EventSystem.Current.FireEvent(new PlayerHitEvent(collision.gameObject, damage));
            //collision.gameObject.GetComponent<PlayerKontroller3D>().GetGameController().TakeDamage(damage);
            Debug.Log("player toke damage");
        }
        //gör skit, explosion, skada etc...
        Destroy(gameObject);
    }
    void Update()
    {
        speed += Time.deltaTime;

        speed = speed % 5f;

        transform.position = MathParabola.Parabola(start, target, 5f, speed / 5f);
    }
}
