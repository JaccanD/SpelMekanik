using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private bool isPaused;
    [SerializeField] private bool controller;

    [SerializeField] private GameObject firstSelected;
    private void Start()
    {
        Controlls.UsingController = controller;
    }
    void Update()
    {
        if (Input.GetKeyDown(Controlls.GetKeyBinding(Function.OpenMenu)))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                ActivateMenu();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                DeactivateMenu();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void ActivateMenu()
    {
        Time.timeScale = 0;
        PauseMenuUI.SetActive(true);

        // Unity Eventsystem
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected);

    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;

        PauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    public void Huvudmeny ()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void DoQuit()
    {
        Application.Quit();
    }
}
