using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
// Author: Hanna :)
public class NPC : MonoBehaviour
{
    [SerializeField] GameController Controller;
    [SerializeField] private string[] dialog;
    [SerializeField] private int RequiredBerries;
    [SerializeField] private GameObject Target;
    [SerializeField] private float TalkRadius = 2.5f;
    [SerializeField] private GameObject RespawnPoint;

    int currentDialog = 0;
    private bool QuestDone = false;

    [SerializeField] private GameObject TalkPrefab;
    private GameObject TalkMarker;

    private void Update()
    {
        TalkMarker.SetActive(false);
        Collider[] colliders = Physics.OverlapSphere(transform.position, TalkRadius);
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
        if(Controller.Berries >= RequiredBerries && !QuestDone)
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
        if (RespawnPoint != null)
            EventSystem.Current.FireEvent(new RespawnPointReachedEvent(RespawnPoint));
        EventSystem.Current.FireEvent(new QuestDoneEvent(gameObject));
    }
}
