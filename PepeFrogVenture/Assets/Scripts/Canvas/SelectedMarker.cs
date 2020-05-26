using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectedMarker : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;
    private GameObject selected;

    private void Awake()
    {
        selected = firstSelected;
<<<<<<< HEAD
    }
    private void Update()
    {
        if (selected == EventSystem.current.currentSelectedGameObject)
=======
        MoveMarker();
    }
    private void Update()
    {
        if (selected == EventSystem.current.currentSelectedGameObject && selected.tag != "Slider")
>>>>>>> 8e3a01e99abae22e94c7e50df7626acb1684cd00
            return;

        selected = EventSystem.current.currentSelectedGameObject;
        MoveMarker();
    }

    private void MoveMarker()
    {
<<<<<<< HEAD
        transform.position = selected.transform.position + Vector3.left * (2 + selected.GetComponent<RectTransform>().sizeDelta.x);
=======
        if(selected.tag == "Slider")
        {
            GameObject handle = selected.transform.Find("Handle Slide Area").Find("Handle").gameObject;
            transform.position = handle.transform.position + Vector3.left * (25 + selected.GetComponent<RectTransform>().sizeDelta.x) * selected.GetComponent<RectTransform>().localScale.x;
        }
        else
            transform.position = selected.transform.position + Vector3.left * (2 + selected.GetComponent<RectTransform>().sizeDelta.x) * selected.GetComponent<RectTransform>().localScale.x;
>>>>>>> 8e3a01e99abae22e94c7e50df7626acb1684cd00
    }
}
