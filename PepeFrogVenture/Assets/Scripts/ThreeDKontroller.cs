using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ******************************
 * -----> 3D Kontroller <--------
 * ******************************
 */
public class ThreeDKontroller : MonoBehaviour
{
    CapsuleCollider Coll;
    [SerializeField] GameObject Camera;
    float RotationX = 0;
    float RotationY = 0;
    [SerializeField] Vector3 CameraDistance;
    [SerializeField] float MaxRotationX;
    [SerializeField] float MinRotationX;
    [SerializeField] float Acceleration = 3.0f;
    [SerializeField] float SkinWidth = 0.2f;
    [SerializeField] LayerMask CollisionMask;
    [SerializeField] float Gravity = 9.82f;
    Vector3 Velocity;
    [SerializeField] float StaticFriktionKoeficcent = 0.1f;
    [SerializeField] float DynamicFriktionKoeficcent = 0.05f;
    [SerializeField] float AirResistanceKoeficent = 0.5f;
    [SerializeField] float MaxSpeed = 8.0f;

    [SerializeField] float MouseSensitivity = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        Coll = GetComponent<CapsuleCollider>();
        Velocity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        Vector3 falling = Gravity * Vector3.down * Time.deltaTime;
        Velocity += falling;

        Velocity *= Mathf.Pow(AirResistanceKoeficent, Time.deltaTime);
        Vector3 nextMove = CheckCollision(Velocity * Time.deltaTime);
        transform.position += nextMove;
    }
    private void GetInput()
    {
        //Rotera kameran mot musen
        Vector2 mouse = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        RotationX -= mouse.y * MouseSensitivity;
        RotationY += mouse.x * MouseSensitivity;
        RotationX = Mathf.Clamp(RotationX, MinRotationX, MaxRotationX);
        Camera.transform.rotation = Quaternion.Euler(RotationX, RotationY, 0.0f);
        //Sätt rörelseriktningen mot hållet kameran tittar
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        direction = Camera.transform.rotation * direction;
        //Projecerar riktningen på planet spelaren står på
        Vector3 topPoint = transform.position + Coll.center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 botPoint = transform.position + Coll.center + Vector3.down * (Coll.height / 2 - Coll.radius);
        RaycastHit cast;
        bool hit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, Vector3.down, out cast, SkinWidth + Coll.height, CollisionMask, QueryTriggerInteraction.Ignore);
        direction = Vector3.ProjectOnPlane(direction, cast.normal).normalized;
        //Flyttar kameran
        MoveCamera();

        if (direction.magnitude > 1)
            direction = direction.normalized;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Velocity += new Vector3(0, 3.0f, 0);
        }
        Velocity += direction * Acceleration * Time.deltaTime;
    }
    private void MoveCamera()
    {
        Vector3 newPosition = Camera.transform.rotation * CameraDistance + transform.position;
        Vector3 castVector = newPosition - transform.position;
        RaycastHit cast;
        bool hit = Physics.SphereCast(transform.position, Camera.GetComponent<SphereCollider>().radius, castVector.normalized, out cast, castVector.magnitude, CollisionMask, QueryTriggerInteraction.Ignore);
        if (hit)
        {
            newPosition = castVector.normalized * cast.distance + transform.position;
        }
        Camera.transform.position = newPosition;
    }
    private bool GroundCheck()
    {
        Vector3 topPoint = transform.position + Coll.center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 botPoint = transform.position + Coll.center + Vector3.down * (Coll.height / 2 - Coll.radius);
        return Physics.CapsuleCast(topPoint, botPoint, Coll.radius, Vector3.down, SkinWidth * 2, CollisionMask);
    }
    private Vector3 CheckCollision(Vector3 startingVelocity)
    {
        if (startingVelocity.magnitude < 0.001f)
        {
            return Vector3.zero;
        }
        Vector3 topPoint = transform.position + Coll.center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 botPoint = transform.position + Coll.center + Vector3.down * (Coll.height / 2 - Coll.radius);
        RaycastHit cast;
        bool hit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, startingVelocity.normalized, out cast, float.MaxValue, CollisionMask, QueryTriggerInteraction.Ignore);
        if (hit)
        {
            bool SkinWidthHit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, -cast.normal, out RaycastHit SkinWidthCast, SkinWidth + startingVelocity.magnitude, CollisionMask, QueryTriggerInteraction.Ignore);
            if(cast.distance > startingVelocity.magnitude + SkinWidth)
            {
                bool groundHit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, startingVelocity.normalized, out RaycastHit groundHitCast, float.MaxValue, CollisionMask, QueryTriggerInteraction.Ignore);
                bool normalHit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, startingVelocity.normalized, out RaycastHit normalHitCast, float.MaxValue, CollisionMask, QueryTriggerInteraction.Ignore);

                Velocity += PhysicsFunctions.NormalForce(Velocity, normalHitCast.normal);
                return startingVelocity;
            }
            Vector3 normalForce = PhysicsFunctions.NormalForce(Velocity, cast.normal);
            Velocity += normalForce;
            ApplyFriktion(normalForce);
            if (SkinWidthHit) transform.position += (Vector3)(-SkinWidthCast.normal) * (SkinWidthCast.distance - SkinWidth);
            return CheckCollision(Velocity * Time.deltaTime);
        }
        else
        {
            return startingVelocity;
        }
    }
    private void ApplyFriktion(Vector3 normalForce)
    {
        if (normalForce.magnitude * StaticFriktionKoeficcent > Velocity.magnitude)
            Velocity = Vector3.zero;
        else
        {
            Velocity += normalForce.magnitude * DynamicFriktionKoeficcent * -Velocity.normalized;
        }
    }
    private void ApplyFriktion(Vector3 normalForce, Vector3 SpeedDifference)
    {
        if (normalForce.magnitude * StaticFriktionKoeficcent > Velocity.magnitude)
            Velocity = SpeedDifference;
        else
        {
            Debug.Log("False");
            Velocity += normalForce.magnitude * DynamicFriktionKoeficcent * -Velocity.normalized;
        }
    }
}
