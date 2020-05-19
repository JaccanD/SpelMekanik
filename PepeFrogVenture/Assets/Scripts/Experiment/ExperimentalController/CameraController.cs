using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private Vector3 cameraOffset;

    [SerializeField] private float mouseSensitivity = 1.0f;
    [SerializeField] private float maxRotationX = 60;
    [SerializeField] private float minRotationX = -60;
    [SerializeField] private float hidePlayerDistance = 1;
    [SerializeField] private float collisionRadius = 0.3f;
    [SerializeField] private float skinWidth = 0.1f;
    [SerializeField] private float heigthOffset;

    private Vector3 anchor { get { return transform.parent.position + Vector3.up * heigthOffset; } }
    private float rotationX = 0;
    private float rotationY = 0;
    private MeshRenderer rend;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rend = model.GetComponent<MeshRenderer>();
        rotationY = transform.parent.rotation.eulerAngles.y;
    }
    void LateUpdate()
    {
        RotateCamera();
        MoveCamera();
    }
    void RotateCamera()
    {
        Vector2 mouse = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        rotationX -= mouse.y * mouseSensitivity;
        rotationY += mouse.x * mouseSensitivity;
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
        
        // TODO
        // Göm spelaren om den är för nära kameran
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
    // TODO 
    // Något sätt att rikta spelaren när den respawnar
}
