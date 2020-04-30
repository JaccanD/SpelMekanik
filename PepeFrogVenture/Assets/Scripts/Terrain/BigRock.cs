using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class BigRock : MonoBehaviour
{
    [SerializeField] private GameObject NPCblocker;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Current.RegisterListener(typeof(QuestDoneEvent), BLASTOFF);
    }

    private void BLASTOFF(Callback.Event eb)
    {
        QuestDoneEvent e = (QuestDoneEvent)eb;
        if(e.NPC != NPCblocker)
        {
            return;
        }
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 4000);
        Destroy(gameObject, 10f);
    }
}
