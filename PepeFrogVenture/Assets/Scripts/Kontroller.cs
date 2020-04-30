using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ******************************
 * -----> 2D Kontroller <--------
 * ******************************
 */
public class Kontroller : MonoBehaviour
{
    int prevCount = 0;
    //Collision
    BoxCollider2D Coll;
    public LayerMask ColliderMask;
    [SerializeField] float SkinWidth = 0.5f;
    [SerializeField] float GroundCheckDistance = 0.2f; // skinWidth + 0.075f ?
    //Movement
    Vector2 Velocity = new Vector2(0, 0);
    [SerializeField] float Gravity = 4.0f;
    [SerializeField] float Acceleration = 8.0f;
    [SerializeField] float MaxSpeed = 24.0f;
    //[SerializeField] float Decceleration = 1.0f;
    [SerializeField] float StaticFriktionKoeficcent = 0.4f;
    [SerializeField] float DynamicFriktionKoeficcent = 0.25f;
    [SerializeField] float AirResistanceKoeficcent = 0.5f;
    [SerializeField] float JumpForce = 30.0f;

    void Start()
    {

    }
    private void Awake()
    {
        Coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("New Update");
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0.0f);
        Velocity += direction * Acceleration * Time.deltaTime;

        Vector2 falling = Gravity * Vector2.down * Time.deltaTime;
        Velocity += falling;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Velocity += Vector2.up * JumpForce;
        }

        Velocity *= Mathf.Pow(AirResistanceKoeficcent, Time.deltaTime);
        Debug.Log(Velocity);
        Vector2 nextMove = CheckCollision(Velocity * Time.deltaTime);
        transform.position += (Vector3) nextMove;
    }
    private Vector2 GetInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector2 direction = new Vector2(horizontal, 0.0f);
        return direction;
    }
    private void Accelerate(Vector2 direction)
    {
    }
    private void Deccelerate()
    {
    }
    private bool GroundCheck()
    {
        RaycastHit2D cast = Physics2D.BoxCast(transform.position, Coll.size, 0.0f, Vector2.down, GroundCheckDistance, ColliderMask);
        if (cast)
        {
            return true;
        }

        return false;
    }
    private void ApplyFriktion(Vector2 normalForce)
    {
        if(normalForce.magnitude * StaticFriktionKoeficcent > Velocity.magnitude)
            Velocity = Vector2.zero;
        else
        {
            Velocity += normalForce.magnitude * DynamicFriktionKoeficcent * -Velocity.normalized;
        }
    }
    private Vector2 CheckCollision(Vector2 startingVelocity)
    {
        if (startingVelocity.magnitude < 0.001f)
        {
            return Vector2.zero;
        }
        RaycastHit2D cast = Physics2D.BoxCast(transform.position, Coll.size, 0.0f, startingVelocity.normalized, startingVelocity.magnitude + SkinWidth, ColliderMask);
        if (cast)
        {
            Debug.DrawLine(transform.position + new Vector3(0, -0.5f, 0), transform.position + new Vector3(0, -0.5f, 0) + (Vector3)((startingVelocity.magnitude + SkinWidth) * startingVelocity.normalized), Color.black, 5.0f, false);
        }
        else
        {
            Debug.DrawLine(transform.position + new Vector3(0, -0.5f, 0), transform.position + new Vector3(0, -0.5f, 0) + (Vector3)((startingVelocity.magnitude + SkinWidth) * startingVelocity.normalized), Color.red, 5.0f, false);
        }
        if (cast)
        {

            RaycastHit2D SkinWidthCast = Physics2D.BoxCast(transform.position, Coll.size, 0.0f, -cast.normal, SkinWidth + startingVelocity.magnitude, ColliderMask);
            if (SkinWidthCast)
            {
                Debug.DrawLine(transform.position + new Vector3(0, -0.5f, 0), transform.position + new Vector3(0, -0.5f, 0) + (Vector3)((startingVelocity.magnitude + SkinWidth) * -cast.normal), Color.blue, 5.0f, false);
            }
            else
            {
                Debug.DrawLine(transform.position + new Vector3(0, -0.5f, 0), transform.position + new Vector3(0, -0.5f, 0) + (Vector3)((startingVelocity.magnitude + SkinWidth) * -cast.normal), Color.green, 5.0f, false);
            }

            Vector2 normalForce = PhysicsFunctions.NormalForce(Velocity, cast.normal);
            Velocity += normalForce;

            ApplyFriktion(normalForce);
            if(SkinWidthCast) transform.position += (Vector3)(-SkinWidthCast.normal) * (SkinWidthCast.distance - SkinWidth);

            return CheckCollision(Velocity * Time.deltaTime);
        }
        else
        {

            return startingVelocity;
        }
    }
}

