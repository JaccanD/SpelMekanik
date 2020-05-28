using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkController : MonoBehaviour
{
    [SerializeField] private LayerMask talkMask;
    
    private float talkRange = 0.1f;
    private void Update()
    {
        if (Input.GetKeyDown(Controlls.GetKeyBinding(Function.Interact)))
        {
            Collider[] npcCheck = Physics.OverlapSphere(transform.position, talkRange, talkMask, QueryTriggerInteraction.Collide);
            if (npcCheck.Length != 0)
            { 
                // TODO
                // Ändra till event istället
                npcCheck[0].gameObject.GetComponent<NPC>().Talk();
            }
        }
    }
}
