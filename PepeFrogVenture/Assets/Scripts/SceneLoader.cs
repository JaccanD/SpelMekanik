using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Author: Hanna Lindberg Johansson
public class SceneLoader : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ControllHelp()
    {
        SceneManager.LoadScene(1);
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
        SceneManager.LoadScene(6);
    }
}
