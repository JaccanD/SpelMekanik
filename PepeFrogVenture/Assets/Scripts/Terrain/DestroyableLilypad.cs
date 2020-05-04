using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Valter Falsterljung
public class DestroyableLilypad : MonoBehaviour
{
    new private BoxCollider collider;

    [SerializeField] private float destructionTimer = 1.5f;


    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }

    public void BossTarget()
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.black;
        StartCoroutine(waitForDestruction());
    }

    IEnumerator waitForDestruction()
    {
        yield return new WaitForSeconds(destructionTimer);
        DestroyLilypad();
    }

    private void DestroyLilypad()
    {
        EventSystem.Current.FireEvent(new LilyPadDestroyedEvent(this.gameObject));
        Destroy(gameObject);
    }
}
