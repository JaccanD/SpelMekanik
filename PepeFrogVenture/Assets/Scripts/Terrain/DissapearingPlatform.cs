using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour
{
    [SerializeField] BoxCollider coll;
    [SerializeField] Material color;
    [SerializeField] float DissapearDelay;
    [SerializeField] float ReapearDelay;

    private MeshCollider CollisionColl;
    private Material StartingMaterial;
    private void Awake()
    {
        StartingMaterial = GetComponent<MeshRenderer>().material;
        CollisionColl = GetComponent<MeshCollider>();
    }
    void Update()
    {
        Collider[] overLaps = Physics.OverlapBox(transform.position + coll.center, coll.bounds.extents, transform.rotation);
        for (int i = 0; i < overLaps.Length; i++)
        {
            if (overLaps[i].transform.gameObject.tag == "Player")
            {
                GetComponent<MeshRenderer>().material = color;
                Invoke("Dissapear", DissapearDelay);
                coll.enabled = false;
                break;
            }
        }
    }

    void Reapear()
    {
        //transform.position += Vector3.up * 100;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        CollisionColl.enabled = true;
        coll.enabled = true;
        GetComponent<MeshRenderer>().material = StartingMaterial;
    }
    void Dissapear()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        CollisionColl.enabled = false;
        //transform.position += Vector3.down * 100;
        Invoke("Reapear", ReapearDelay);
    }
}
