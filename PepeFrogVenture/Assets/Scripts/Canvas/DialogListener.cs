using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Callback;
// Author: Jacob Didenbäck
public class DialogListener : MonoBehaviour
{
    [SerializeField] private GameObject Text;
    void Start()
    {
        EventSystem.Current.RegisterListener(typeof(NPCDialogueEvent), UpdateText);
        Text.SetActive(false);

    }

    public void UpdateText(Callback.Event eb)
    {
        NPCDialogueEvent e = (NPCDialogueEvent)eb;
        Text.SetActive(true);
        Text.GetComponent<Text>().text = e.Text;

        Invoke("RemoveText", 8);
    }

    public void RemoveText()
    {
        Text.SetActive(false);
    }
}
