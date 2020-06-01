using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animation animation;
    [SerializeField] private int sceneToLoad;
    [SerializeField] private float transistionTime = 1;
    
    public void LoadLevel()
    {

    }

    IEnumerator WaitToLoadLevel()
    {
        yield return new WaitForSeconds(transistionTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
