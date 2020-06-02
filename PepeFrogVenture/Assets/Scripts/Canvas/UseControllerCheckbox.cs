using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseControllerCheckbox : MonoBehaviour
{
    Toggle button;

    [Header ("Image Settings")]
    [SerializeField] private Image target;
    [SerializeField] private Sprite keyboardImage;
    [SerializeField] private Sprite controllerImage;
    [SerializeField] private GameObject controllerImageText;
    [SerializeField] private GameObject keyBoardImageText;


    private void Awake()
    {
        button = GetComponent<Toggle>();
        button.onValueChanged.AddListener(delegate {
            SetUsingController(button);

        });

        button.isOn = Controlls.UsingController;

        SetImage(button.isOn);
    }

    void SetUsingController(Toggle changed)
    {
        Controlls.UsingController = changed.isOn;
        Debug.Log(Controlls.UsingController);
        SetImage(changed.isOn);
    }

    void SetImage(bool controllType)
    {
        if (controllType == true)
        {
            controllerImageText.SetActive(true);
            keyBoardImageText.SetActive(false);
            target.sprite = controllerImage;
        }

        else
        {
            controllerImageText.SetActive(false);
            keyBoardImageText.SetActive(true);
            target.sprite = keyboardImage;
        }
    }
    
}
