using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private bool isPaused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivateMenu();
        } 
        else
        {
            DeactivateMenu();
        }
    }

    private void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        PauseMenuUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        PauseMenuUI.SetActive(false);
        isPaused = false;
    }
}
