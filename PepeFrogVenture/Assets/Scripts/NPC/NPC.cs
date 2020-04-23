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

    private void Awake()
    {
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
        if(Controller.Berries == RequiredBerries)
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
        Controller.RemoveBerries();
    }
}
