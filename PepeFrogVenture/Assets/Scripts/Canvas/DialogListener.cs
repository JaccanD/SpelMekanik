using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Callback;

public class DialogListener : MonoBehaviour
{
    [SerializeField] private GameObject Text;
    void Start()
    {
        EventSystem.Current.RegisterListener<NPCDialogueEvent>(UpdateText);
        Text.SetActive(false);

    }

    public void UpdateText(NPCDialogueEvent e)
    {
        Text.SetActive(true);
        Text.GetComponent<Text>().text = e.Text;

        Invoke("RemoveText", 8);
    }

    public void RemoveText()
    {
        Text.SetActive(false);
    }
}
