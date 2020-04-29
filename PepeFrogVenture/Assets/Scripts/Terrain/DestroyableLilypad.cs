using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class DestroyableLilypad : MonoBehaviour
{
    new private BoxCollider collider;
    //[SerializeField] private bool IsSinking = false;
    [SerializeField] private float destructionTimer = 1.5f;
    //private float sinkingSpeed = 1.5f;
    // Start is called before the first frame update

    //public bool getIsSinking()
    //{
    //    return IsSinking;
    //}
    //public void setIsSInking(bool value)
    //{
    //    IsSinking = value;
    //}

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }

    //void Update()
    //{
    //    if (IsSinking)
    //    {
    //        DestroyLilypad();
    //    }
    //}
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
