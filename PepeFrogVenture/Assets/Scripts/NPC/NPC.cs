using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
// Author: Jacob Didenbäck
public class NPC : MonoBehaviour
{
    [SerializeField] GameController Controller;
    [SerializeField] private string[] dialog;
    [SerializeField] private int RequiredBerries;
    [SerializeField] private GameObject Target;
    int currentDialog = 0;
    private bool QuestDone = false;

    [SerializeField] private GameObject TalkPrefab;
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
        TalkMarker = GameObject.Instantiate(TalkPrefab, transform.position, transform.rotation);
        TalkMarker.transform.position = transform.position + Vector3.up;
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
    }

    private void Unlock()
    {
        QuestDone = true;
        Target.SetActive(false);
        EventSystem.Current.FireEvent(new QuestDoneEvent(gameObject));
    }
}
