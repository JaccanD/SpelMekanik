using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Author: Jacob Didenbäck
public class LevelTransition : MonoBehaviour
{
    private BoxCollider Coll;
    [SerializeField] private int LevelIndex;

    private void Start()
    {
        Coll = GetComponent<BoxCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        Collider[] overLaps = Physics.OverlapBox(transform.position + Coll.center, Coll.bounds.extents, transform.rotation);

        for (int i = 0; i < overLaps.Length; i++)
        {
            
            if (overLaps[i].transform.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(LevelIndex);
                break;
            }
        }
    }
}
