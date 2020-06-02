using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private Vector3 cameraOffset;

    [SerializeField] private float maxRotationX = 60;
    [SerializeField] private float minRotationX = -60;
    [SerializeField] private float hidePlayerDistance = 1;
    [SerializeField] private float collisionRadius = 0.3f;
    [SerializeField] private float skinWidth = 0.1f;
    [SerializeField] private float heigthOffset;
    [Header ("Mouse and Keyboard")]
    [SerializeField] private float mouseSensitivity = 1.0f;

    [Header("Controller")]
    [Range (1 , 6)][SerializeField] private float horizontalStickSensitivity;
    [Range (1 , 6)][SerializeField] private float verticalStickSensitivity;
    [Header ("AutoAim")]
    [Tooltip ("How far away the enemy can be before the autoaim ignores it")][SerializeField] private float autoAimRange;
    [Tooltip ("How far from the crosshair the autoaim can \"see\"")][Range (0 , 4)][SerializeField] private float autoAimRadius;
    [SerializeField] private LayerMask enemyMask;

    private Vector3 anchor { get { return transform.parent.position + Vector3.up * heigthOffset; } }
    private float rotationX = 0;
    private float rotationY = 0;
    private SkinnedMeshRenderer rend;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rend = model.GetComponent<SkinnedMeshRenderer>();
        rotationY = transform.parent.rotation.eulerAngles.y;
        EventSystem.Current.RegisterListener(typeof(PlayerRespawnEvent), OnRespawn);
    }
    void LateUpdate()
    {
        if (Time.timeScale != 1)
            return;
        RotateCamera();
        MoveCamera();
        AutoAim();
    }
    void RotateCamera()
    {
        GetRotation();
        rotationX = Mathf.Clamp(rotationX, minRotationX, maxRotationX);
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0.0f);

        transform.parent.rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
    }
    void MoveCamera()
    {
        Vector3 newPosition = transform.rotation * cameraOffset + anchor;
        Vector3 castVector = newPosition - anchor;
        RaycastHit cast;
        bool hit = Physics.SphereCast(anchor, collisionRadius + skinWidth, castVector.normalized, out cast, castVector.magnitude, collisionMask);
        
        if (hit)
        {
            if (cast.distance < hidePlayerDistance)
            {
                rend.enabled = false;
            }
            else
            {
                rend.enabled = true;
            }
            newPosition = castVector.normalized * cast.distance + anchor;
        }
        else
        {
            rend.enabled = true;
        }

        transform.position = newPosition;
    }
    void GetRotation()
    {
        if (Controlls.UsingController)
        {
            Vector2 joystick = new Vector2(Input.GetAxisRaw("RightStickHorizontal"), Input.GetAxisRaw("RightStickVertical"));
            rotationX += joystick.y * verticalStickSensitivity;
            rotationY += joystick.x * horizontalStickSensitivity;
        }
        else
        {
            Vector2 mouse = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            rotationX -= mouse.y * mouseSensitivity;
            rotationY += mouse.x * mouseSensitivity;
        }
    }
    // TODO
    // Auto aim om man använder kontroll

    void AutoAim()
    {
        // Använd inte autoAim om spelaren spelar med mus
        if (!Controlls.UsingController) return;

        Vector2 joystick = new Vector2(Input.GetAxisRaw("RightStickHorizontal"), Input.GetAxisRaw("RightStickVertical")).normalized;

        // Använd inte autoAim
        // Om spelaren trycker tillräckligt hårt på joysticken 
        if (joystick.magnitude > 0.6f) return;


        // Hitta alla fiender nära siktet
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, autoAimRadius, transform.rotation * Vector3.forward, autoAimRange, enemyMask, QueryTriggerInteraction.Ignore);
        if (hits.Length == 0)
        {
            return;
        }
        // Hitta närmaste fienden
        RaycastHit closest = FindClosestHit(hits);
        // Hitta vinkeln mellan camerans riktning och riktningen till närmaste fienden
        Vector3 midPoint = Vector3.Lerp(closest.point, closest.transform.position, 0.25f);
        Vector3 toClosest = midPoint - transform.position;

        Quaternion newRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(toClosest), 0.5f);
        transform.rotation = newRotation;
        //transform.rotation = Quaternion.LookRotation(toClosest);
        rotationX = transform.rotation.eulerAngles.x;
        rotationY = transform.rotation.eulerAngles.y;
        MoveCamera();

        // sätt rotationen till den vinkeln
        // Om vinkeln är tillräckligt liten 
    }
    private RaycastHit FindClosestHit(RaycastHit[] hits)
    {
        if (hits.Length == 1) return hits[0];

        int returnIndex = 0;
        Vector3 toHit = hits[0].transform.position - transform.position;
        float dot = Vector3.Dot(toHit, transform.rotation * Vector3.forward);
        for (int i = 1; i < hits.Length; i++)
        {
            toHit = hits[i].transform.position - transform.position;
            float nextDot = Vector3.Dot(toHit, transform.position);
            if (dot < nextDot)
            {
                dot = nextDot;
                returnIndex = i;
            }
        }
        return hits[returnIndex];
    }
    // TODO 
    // Något sätt att rikta spelaren när den respawnar

    void OnRespawn(Callback.Event eb)
    {
        PlayerRespawnEvent e = (PlayerRespawnEvent)eb;

        rotationX = e.RespawnPoint.transform.rotation.eulerAngles.x;
        rotationY = e.RespawnPoint.transform.rotation.eulerAngles.y;

        rotationX = Mathf.Clamp(rotationX, minRotationX, maxRotationX);
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0.0f);

        transform.parent.rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
    }
}
