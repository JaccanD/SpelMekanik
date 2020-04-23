using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
/*
 * ******************************
 * -----> 3D Kontroller <--------
 * ******************************
 */
public class PlayerKontroller3D : MonoBehaviour
{
    private StateMachine stateMachine;
    public State[] states;

    private Vector3 Velocity = Vector3.zero; 
    private CapsuleCollider Coll;
    [SerializeField] private float SkinWidth = 0.1f;
    [SerializeField] LayerMask CollisionMask;
    [SerializeField] LayerMask CameraMask;
    private float RotationX = 0;
    private float RotationY = 0;
    [SerializeField] private float MouseSensitivity = 1;
    [SerializeField] private float MinRotationX = -60;
    [SerializeField] private float MaxRotationX = 60;
    [SerializeField] private GameObject Camera;
    [SerializeField] Vector3 CameraDistance;
    [SerializeField] private float GroundCheckDistance;
    [SerializeField] private LayerMask HookMask;
    [SerializeField] private float ToungeLength;
    [SerializeField] private LayerMask PickUpMask;
    [SerializeField] private GameController Controller;
    [SerializeField] private GameObject Fireball;
    [SerializeField] LayerMask TalkMask;
    [SerializeField] GameObject ToungePrefab;
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
    void Start()
    {
        Coll = GetComponent<CapsuleCollider>();
        stateMachine = new StateMachine(this, states);
        Cursor.lockState = CursorLockMode.Locked;
        EventSystem.Current.RegisterListener<PlayerDeathEvent>(Die);
        EventSystem.Current.RegisterListener<HookHitEvent>(Pull);
    }
    void Update()
    {
        GetInput();
        stateMachine.Run();    
    }
    public void GetInput()
    {
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
        Vector3 topPoint = transform.position + Coll.center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 botPoint = transform.position + Coll.center + Vector3.down * (Coll.height / 2 - Coll.radius);
        RaycastHit cast;
        bool hit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, Vector3.down, out cast, SkinWidth + Coll.height, CollisionMask, QueryTriggerInteraction.Ignore);
        if (hit)
            direction = Vector3.ProjectOnPlane(direction, cast.normal).normalized;
        else
            direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;
        if(Controller.fire && Input.GetKeyDown(KeyCode.Q))
        {
            GameObject fireball = Instantiate(Fireball, topPoint, Camera.transform.rotation);
            Controller.fire = false;
        }
        //Flyttar kameran
        MoveCamera();

        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }
        Direction = direction;
    }
    private void MoveCamera()
    {
        Vector3 newPosition = Camera.transform.rotation * CameraDistance + transform.position;
        Vector3 castVector = newPosition - transform.position;
        RaycastHit cast;
        bool hit = Physics.SphereCast(transform.position, Camera.GetComponent<SphereCollider>().radius, castVector.normalized, out cast, castVector.magnitude, CameraMask/*CollisionMask*/);
        if (hit)
        {
            newPosition = castVector.normalized * cast.distance + transform.position;
        }
        Camera.transform.position = newPosition;
    }
    public void Die(PlayerDeathEvent e)
    {
        stateMachine.TransitionTo<PlayerDeadState>();
    }
    public void Pull(HookHitEvent e)
    {
        Hook = e.Point;
        stateMachine.TransitionTo<PlayerSwingState>();
    }

}
