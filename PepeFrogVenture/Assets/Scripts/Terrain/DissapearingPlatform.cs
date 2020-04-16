using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour
{
    [SerializeField] BoxCollider coll;
    [SerializeField] Material color;

    private Material StartingMaterial;
    void Update()
    {
        Collider[] overLaps = Physics.OverlapBox(transform.position + coll.center, coll.bounds.extents, transform.rotation);
        for (int i = 0; i < overLaps.Length; i++)
        {
            if (overLaps[i].transform.gameObject.tag == "Player")
            {
                StartingMaterial = GetComponent<MeshRenderer>().material;
                GetComponent<MeshRenderer>().material = color;
                Invoke("Dissapear", 3);
                coll.enabled = false;
                break;
            }
        }
    }

    void Reapear()
    {
        transform.position += Vector3.up * 100;
        coll.enabled = true;
        GetComponent<MeshRenderer>().material = StartingMaterial;
    }
    void Dissapear()
    {
        transform.position += Vector3.down * 100;
        Invoke("Reapear", 6);
    }
}
