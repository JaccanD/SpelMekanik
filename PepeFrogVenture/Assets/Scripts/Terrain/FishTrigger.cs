using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTrigger : MonoBehaviour
{
    [SerializeField] private GameObject fish;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            fish.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
