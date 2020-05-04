using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Author: Valter Falsterljung
public class DabZOne : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("eneter");
        SceneManager.LoadScene("LvL1terrain");
    }
}
