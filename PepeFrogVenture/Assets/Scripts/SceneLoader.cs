using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
