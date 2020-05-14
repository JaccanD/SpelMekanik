using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class ToungeController : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private PlayerControl parent;
    [SerializeField] private float toungeLength;
    [SerializeField] private LayerMask hookMask;
    [SerializeField] new private GameObject camera;

    private bool toungeReady = true;
    private Vector3 topPoint { get { return transform.position + Vector3.up * (parent.Collider.height - parent.Collider.height); } }
    private Vector3 forward { get { return camera.transform.rotation * Vector3.forward; } }
    private Vector3 cameraPosition { get { return camera.transform.position; } }
    private void start()
    {
        if (parent == null)
        {
            this.enabled = false;
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(Controlls.GetKeyBinding(Function.ShootTounge)))
        {
            ToungeFlick();
        }
    }

    protected void ToungeFlick()
    {
        if (!toungeReady)
        {
            return;
        }
        //Skicka event att vi använder tungan.
        //Spawna tungan i munnen
        //Tungan sträcker ut sig tills den träffar något
        
        bool hookHit = Physics.SphereCast(cameraPosition, 0.3f, forward, out RaycastHit HookCast, toungeLength, hookMask);
        if (!hookHit || HookCast.distance < (cameraPosition - topPoint).magnitude)
        {
            return;
        }
        //EventSystem.Current.FireEvent(new ToungeFlickEvent());
        Vector3 end = HookCast.point;
        Vector3 toungeDirection = (end - topPoint).normalized;
        Vector3 rotation = toungeDirection + Vector3.up;
        Quaternion rotate = new Quaternion(rotation.x, rotation.y, rotation.z, 0);
        GameObject go = Instantiate(prefab, topPoint, rotate);
        go.GetComponent<Tounge>().SetPoint(end);

    }
}
