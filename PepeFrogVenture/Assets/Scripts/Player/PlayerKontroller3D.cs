using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
/*
 * ******************************
 * -----> 3D Kontroller <--------
 * ******************************
 */

    // Main Author: Jacob Didenbäck
    // Secondary Author: Valter Falsterljung
public class PlayerKontroller3D : MonoBehaviour
{
    private StateMachine stateMachine;
    public State[] states;
    private Vector3 Velocity = Vector3.zero; 
    private CapsuleCollider Coll;
    private float RotationX = 0;
    private float RotationY = 90;
    private GameController Controller;
    private Vector3 Center { get { return GetComponent<CapsuleCollider>().center + transform.position; } }
    private bool IsStunned;
    [SerializeField] private GameObject Fireball;
    [SerializeField] LayerMask TalkMask;

    [Header ("Movement Settings")]
    [SerializeField] LayerMask CollisionMask;
    [SerializeField] private float SkinWidth = 0.1f;
    [SerializeField] private float GroundCheckDistance;
    [SerializeField] private float StunnedTime = 1f;

    [Header ("Tounge Settings")]
    [SerializeField] private LayerMask HookMask;
    [SerializeField] private float ToungeLength;
    [SerializeField] private LayerMask PickUpMask;
    [SerializeField] GameObject ToungePrefab;

    [Header("Camera Settings")]
    [SerializeField] private GameObject Camera;
    [SerializeField] Vector3 CameraDistance;
    [SerializeField] LayerMask CameraMask;
    [SerializeField] float CameraHidePlayerDistance;
    [SerializeField] private float MouseSensitivity = 1;
    [SerializeField] private float MinRotationX = -60;
    [SerializeField] private float MaxRotationX = 60;



    private Vector3 Hook;

    private Vector3 Direction = Vector3.zero;

    public Vector3 GetVelocity()
    {
        return Velocity;
    }
    public void SetVelocity(Vector3 newVelocity)
    {
        Velocity = newVelocity;
    }
    public float GetSkinWidth()
    {
        return SkinWidth;
    }
    public LayerMask GetCollisionMask()
    {
        return CollisionMask;

    }
    public CapsuleCollider GetCollider()
    {
        return Coll;
    }
    public float GetGroundCheckDistance()
    {
        return GroundCheckDistance;

    }
    public Transform GetTransform()
    {
        return transform;
    }
    public Vector3 GetDirection()
    {
        return Direction;
    }
    public Vector3 GetHook()
    {
        return Hook;

    }
    public void SetHook(Vector3 newHook)
    {
        Hook = newHook;
    }
    public LayerMask GetHookMask()
    {

        return HookMask;
    }
    public GameObject GetCamera()
    {
        return Camera;
    }
    public float GetToungeLength()
    {
        return ToungeLength;
    }
    public LayerMask GetPickUpMask()
    {
        return PickUpMask;
    }
    public GameController GetGameController()
    {
        return Controller;
    }
    public GameObject GetFireball()
    {
        return Fireball;
    }
    public LayerMask GetTalkMask()
    {
        return TalkMask;
    }
    public GameObject GetToungePrefab()
    {
        return ToungePrefab;
    }
    public Vector3 GetCenter()
    {
        return Center;
    }
    void Start()
    {
        Coll = GetComponent<CapsuleCollider>();
        stateMachine = new StateMachine(this, states);
        GameObject ControllerGo = GameObject.FindGameObjectWithTag("GameController");
        Controller = ControllerGo.GetComponent<GameController>();
        Cursor.lockState = CursorLockMode.Locked;
        EventSystem.Current.RegisterListener(typeof(PlayerDeathEvent), Die);
        EventSystem.Current.RegisterListener(typeof(HookHitEvent), Pull);
        EventSystem.Current.RegisterListener(typeof(Pushed), IsPushed);
        EventSystem.Current.RegisterListener(typeof(PlayerRespawnEvent), PlayerRespawned);
    }
    void Update()
    {
        GetInput();
        stateMachine.Run();
    }
    public void GetInput()
    {
        if (IsStunned)
        {
            return;
        }
        //Rotera kameran mot musen
        Vector2 mouse = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        RotationX -= mouse.y * MouseSensitivity;
        RotationY += mouse.x * MouseSensitivity;
        RotationX = Mathf.Clamp(RotationX, MinRotationX, MaxRotationX);
        Camera.transform.rotation = Quaternion.Euler(RotationX, RotationY, 0.0f);

        // Ta bort när vi ska animera!
        transform.rotation = Quaternion.Euler(0.0f, RotationY, 0.0f);

        //Sätt rörelseriktningen mot hållet kameran tittar
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        direction = Camera.transform.rotation * direction;
        //Projecerar riktningen på planet spelaren står på
        Vector3 topPoint = Center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 botPoint = Center + Vector3.down * (Coll.height / 2 - Coll.radius);
        RaycastHit cast;
        bool hit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, Vector3.down, out cast, SkinWidth + Coll.height, CollisionMask, QueryTriggerInteraction.Ignore);
        if (hit)
        {
            direction = Vector3.ProjectOnPlane(direction, cast.normal).normalized;
        }
        else
            direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;
        if (Controller.HasFire && Input.GetKeyDown(KeyCode.Mouse1))
        {

            GameObject fireball = Instantiate(Fireball, topPoint, CalculateFireballRotation());
            EventSystem.Current.FireEvent(new FireballshotEvent());
            Controller.HasFire = false;
        }
        //Flyttar kameran
        MoveCamera();

        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }
        Direction = direction;
        
    }
    public State InState()
    {
        return stateMachine.GetCurrentState();
    }
    private void IsPushed(Callback.Event eb)
    {
        Pushed e = (Pushed) eb;
        Vector3 direction = (e.Player.transform.position - e.Origin).normalized;
        Velocity = direction * e.PushBackStrenght + (Vector3.up * e.Height);
        Stun(e.StunDuration);
    }
    private void Stun(float duration)
    {
        StunnedTime = duration;
        IsStunned = true;
        Direction = Vector3.zero;
        StartCoroutine(StunCountDOwn());

    }
    private void PlayerRespawned(Callback.Event eb)
    {
        stateMachine.TransitionTo<PlayerStandingState>();
    }
    IEnumerator StunCountDOwn()
    {
        yield return new WaitForSeconds(StunnedTime);

        IsStunned = false;
    }
    private void MoveCamera()
    {
        Vector3 newPosition = Camera.transform.rotation * CameraDistance + Center;
        Vector3 castVector = newPosition - Center;
        RaycastHit cast;
        bool hit = Physics.SphereCast(Center, Camera.GetComponent<SphereCollider>().radius + 0.2f, castVector.normalized, out cast, castVector.magnitude, CameraMask/*CollisionMask*/);
        if (hit)
        {
            if (cast.distance < CameraHidePlayerDistance)
            {
                GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            }
            else
            {
                GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            }
            newPosition = castVector.normalized * cast.distance + Center;
        }
        else
        {
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        }
        Camera.transform.position = newPosition;
    }
    public void Die(Callback.Event eb)
    {
        PlayerDeathEvent e = (PlayerDeathEvent)eb;
        stateMachine.TransitionTo<PlayerDeadState>();
    }
    public void Pull(Callback.Event eb)
    {
        HookHitEvent e = (HookHitEvent)eb;
        Hook = e.Point;
        stateMachine.TransitionTo<PlayerSwingState>();
    }
    public Quaternion CalculateFireballRotation()
    {
        Vector3 start = Center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 forward = Camera.transform.rotation * Vector3.forward;
        bool hookHit = Physics.Raycast(Camera.transform.position, Camera.transform.rotation * new Vector3(0, 0, 1), out RaycastHit ShootCast);
        if (!hookHit)
        {
            return Camera.transform.rotation;
        }
        Vector3 end = ShootCast.point;
        Vector3 toungeDirection = (end - start).normalized;
        Vector3 rotation = toungeDirection + Vector3.forward;
        return new Quaternion(rotation.x, rotation.y, rotation.z, 0);
    }

}
