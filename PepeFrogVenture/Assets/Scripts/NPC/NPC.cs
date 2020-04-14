using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] GameController Controller;
    [SerializeField] private string[] dialog;
    [SerializeField] private int RequiredBerries;
    [SerializeField] private GameObject Target;
    int currentDialog = 0;
    private void Awake()
    {
        Target.SetActive(false);
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
        Debug.Log(dialog[currentDialog]);
    }

    private void Unlock()
    {
        Target.SetActive(true);
        Controller.RemoveBerries();
    }
}
