using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class SpittingController : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] new private GameObject camera;
    [SerializeField] private float heightOffset;

    private Vector3 topPoint { get { return transform.position + Vector3.up * heightOffset; } }
    private bool canShoot = false;
    // Update is called once per frame
    private void Awake()
    {
        EventSystem.Current.RegisterListener(typeof(PickupEvent), OnPickup);
    }
    void Update()
    {
        if (!canShoot)
            return;
        else if (Input.GetKeyDown(Controlls.GetKeyBinding(Function.ShootFireball)))
            Shoot();
    }

    public void OnPickup(Callback.Event eb)
    {
        PickupEvent e = (PickupEvent)eb;
        if(e.Pickup.tag == "Fire")
        {
            canShoot = true;
        }
    }
    private void Shoot()
    {
        EventSystem.Current.FireEvent(new FireballshotEvent());
        GameObject fireball = Instantiate(prefab, topPoint, RotateFireball());
        canShoot = false;
    }
    private Quaternion RotateFireball()
    {
        bool hookHit = Physics.Raycast(camera.transform.position, camera.transform.rotation * new Vector3(0, 0, 1), out RaycastHit ShootCast);
        if (!hookHit)
        {
            return camera.transform.rotation;
        }
        Vector3 end = ShootCast.point;
        Vector3 aimDirection = (end - topPoint).normalized;
        Vector3 rotation = aimDirection + Vector3.forward;
        return new Quaternion(rotation.x, rotation.y, rotation.z, 0);
    }
}
