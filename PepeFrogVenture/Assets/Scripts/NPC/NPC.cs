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
    [SerializeField] private float turnSpeed;

    [SerializeField] private AudioSource NPCAudioSource;
    [SerializeField] private AudioClip RecieveBluberry;

    private GameObject player;

    int currentDialog = 0;
    private bool QuestDone = false;

    [SerializeField] private GameObject TalkPrefab;
    private GameObject TalkMarker;

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            RotateTowardsPlayer();
        }
    }
    private void Awake()
    {
        TalkMarker = GameObject.Instantiate(TalkPrefab, transform.position, transform.rotation);
        TalkMarker.transform.position = transform.position + Vector3.up;
        TalkMarker.SetActive(false);
        Target.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
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
            NPCAudioSource.PlayOneShot(RecieveBluberry);
        }
        EventSystem.Current.FireEvent(new NPCDialogueEvent(dialog[currentDialog]));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (TalkMarker != null)
            {
                TalkMarker.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (TalkMarker != null)
            {
                TalkMarker.SetActive(false);
            }
        }
    }
    private void Unlock()
    {
        QuestDone = true;
        Target.SetActive(false);
        if (RespawnPoint != null)
            EventSystem.Current.FireEvent(new RespawnPointReachedEvent(RespawnPoint));
        EventSystem.Current.FireEvent(new QuestDoneEvent(gameObject));
    }

    private void RotateTowardsPlayer()
    {
        Vector3 between = player.transform.position - transform.position;

        between.y = 0;

        Quaternion rotation = Quaternion.LookRotation(between);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
    }
}
