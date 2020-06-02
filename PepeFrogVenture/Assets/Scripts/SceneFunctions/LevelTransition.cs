using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Author: Jacob Didenbäck
// Secondary Author: Valter Fallsterljung
public class LevelTransition : MonoBehaviour
{
    private BoxCollider Coll;
    private bool isLoading;
    [SerializeField] private Animator animation;
    [SerializeField] private float transistionTime = 1;
    [SerializeField] private int LevelIndex;

    private void Start()
    {
        Coll = GetComponent<BoxCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isLoading)
        {
            return;
        }
        Collider[] overLaps = Physics.OverlapBox(transform.position + Coll.center, Coll.bounds.extents, transform.rotation);

        for (int i = 0; i < overLaps.Length; i++)
        {
            
            if (overLaps[i].transform.gameObject.tag == "Player" && !isLoading)
            {

                animation.SetTrigger("End");
                isLoading = true;
                StartCoroutine(WaitToLoadLevel());
            }
        }
    }
    IEnumerator WaitToLoadLevel()
    {
        yield return new WaitForSeconds(transistionTime);
        SceneManager.LoadScene(LevelIndex);
    }
}
