using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseControllerCheckbox : MonoBehaviour
{
    Toggle button;

    private void Awake()
    {
        button = GetComponent<Toggle>();
        button.onValueChanged.AddListener(delegate {
            SetUsingController(button);

        });

        button.isOn = Controlls.UsingController;
    }

    void SetUsingController(Toggle changed)
    {
        Controlls.UsingController = changed.isOn;
    }
    
}
