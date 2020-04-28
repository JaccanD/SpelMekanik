using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class NPC : MonoBehaviour
{
    [SerializeField] GameController Controller;
    [SerializeField] private string[] dialog;
    [SerializeField] private int RequiredBerries;
    [SerializeField] private GameObject Target;
    int currentDialog = 0;
    private bool QuestDone = false;

    private GameObject TalkMarker;

    private void Update()
    {
        TalkMarker.SetActive(false);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject.tag == "Player")
            {
                if(TalkMarker != null)
                {
                    TalkMarker.SetActive(true);
                }
            }
        }
    }
    private void Awake()
    {
        TalkMarker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        TalkMarker.transform.position = transform.position + Vector3.up * 2;
        TalkMarker.SetActive(false);
        Target.SetActive(true);
    }
    public void Talk()
    {
        if (Controller.Berries == 0)
        {
            currentDialog = 0;
        }
        if(Controller.Berries > 0)
        {
            currentDialog = 1;
        }
        if(Controller.Berries == RequiredBerries && !QuestDone)
        {
            currentDialog = 2;
            Unlock();
        }
        if (QuestDone)
        {
            currentDialog = 3;
        }
        EventSystem.Current.FireEvent(new NPCDialogueEvent(dialog[currentDialog]));
        Debug.Log(dialog[currentDialog]);
    }

    private void Unlock()
    {
        QuestDone = true;
        Target.SetActive(false);
        EventSystem.Current.FireEvent(new QuestDoneEvent());
    }
}
