using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Marker : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;
    [SerializeField] private float buttonOffset;
    [SerializeField] private float sliderOffset;

    private GameObject selected;
    private void Start()
    {
        if(Controlls.UsingController == false)
        {
            this.gameObject.SetActive(false);
            return;
        }
        selected = firstSelected;
        MoveMarker();

    }
    private void Update()
    {
        if (selected == EventSystem.current.currentSelectedGameObject && selected.tag != "Slider")

            return;

        selected = EventSystem.current.currentSelectedGameObject;
        MoveMarker();
    }

    private void MoveMarker()
    {
        if(selected.tag == "Slider")
        {
            GameObject handle = selected.transform.Find("Handle Slide Area").Find("Handle").gameObject;
            transform.position = handle.transform.position + Vector3.left * (sliderOffset + selected.GetComponent<RectTransform>().sizeDelta.x) * selected.GetComponent<RectTransform>().localScale.x;
        }
        else
            transform.position = selected.transform.position + Vector3.left * (buttonOffset + selected.GetComponent<RectTransform>().sizeDelta.x) * selected.GetComponent<RectTransform>().localScale.x;
    }

    private void OnEnable()
    {
        selected = EventSystem.current.currentSelectedGameObject;
        if(selected != null)
        MoveMarker();
    }
}


