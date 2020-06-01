using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartMenyMarker : MonoBehaviour
{
    [SerializeField] private Toggle button;

    private void Awake()
    {
        button.onValueChanged.AddListener(delegate {
            SetActivateMarker(button);

        });

        
    }

    void SetActivateMarker(Toggle changed)
    {
        this.gameObject.SetActive(changed.isOn);
    }
}
