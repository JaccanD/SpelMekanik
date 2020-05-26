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
    }
    private void Update()
    {
        if (selected == EventSystem.current.currentSelectedGameObject)
            return;

        selected = EventSystem.current.currentSelectedGameObject;
        MoveMarker();
    }

    private void MoveMarker()
    {
        transform.position = selected.transform.position + Vector3.left * (2 + selected.GetComponent<RectTransform>().sizeDelta.x);
    }
}
