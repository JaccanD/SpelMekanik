using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedMarker : MonoBehaviour
{
    private MarkerMode currentMode;
    private int selected = 0;
    [SerializeField] private GameObject[] menuItems;
    private System.Type[] menuItemsTypes;

    new private RectTransform transform { get { return GetComponent<RectTransform>(); } }
    private GameObject selectedMenuItem { get { return menuItems[selected]; } }

    private void Awake()
    {
        if (Controlls.UsingController == false)
        {
            this.gameObject.SetActive(false);
        }
        MoveMarker();
    }
    private void Update()
    {
        
    }
    private void MoveMarker()
    {
        /// Set markermode
        /// 
        /// Move marker according to mode
        /// 
        SetMarkerMode();

        if(currentMode == MarkerMode.Button)
        {
            RectTransform test = selectedMenuItem.transform.GetChild(0).GetComponent<RectTransform>();
            Debug.Log(test.gameObject.name);
            transform.position = test.position + Vector3.left * test.sizeDelta.x;
        }

        if(currentMode == MarkerMode.Slider)
        {

        }

        if(currentMode == MarkerMode.Error)
        {
            Debug.Log("MarkerMode Error");
        }
    }

    private void SetMarkerMode()
    {
        if (selectedMenuItem.GetComponent<Button>() != null)
        {
            currentMode = MarkerMode.Button;
        }
        else if (selectedMenuItem.GetComponent<Slider>() != null)
        {
            currentMode = MarkerMode.Slider;
        }
        else
        {
            currentMode = MarkerMode.Error;
        }
    }

    private enum MarkerMode
    {
        Button,
        Slider,
        Error
    }
}
