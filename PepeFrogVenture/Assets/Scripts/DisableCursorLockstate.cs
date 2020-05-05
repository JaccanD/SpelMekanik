using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Valter Fallsterljung
public class DisableCursorLockstate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
