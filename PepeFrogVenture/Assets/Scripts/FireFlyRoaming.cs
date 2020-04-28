using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyRoaming : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 moveToPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        moveToPosition = Random.insideUnitSphere * 5;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (moveToPosition - startPosition) * Time.deltaTime;
    }
}
