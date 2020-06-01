using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
// Author: Hanna Lindberg Johansson
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject controllsMeny;
    [SerializeField] private GameObject controllsButton;
    [SerializeField] private GameObject controllerButton;
    
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ControllHelp()
    {
        controllsMeny.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controllerButton);
    }

    public void ExitControlls()
    {
        controllsMeny.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controllsButton);

    }

    public void ReturnButton()
    {
        SceneManager.LoadScene(0);
    }

    public void DoQuit()
    {
        Application.Quit();
    }

    public void BossTwo()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Hedvigs sandbox");
    }
}
