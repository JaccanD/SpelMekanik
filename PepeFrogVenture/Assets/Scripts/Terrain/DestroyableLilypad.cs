using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableLilypad : MonoBehaviour
{
    new private BoxCollider collider;
    [SerializeField] private bool IsSinking = false;
    //private float sinkingSpeed = 1.5f;
    // Start is called before the first frame update

    public bool getIsSinking()
    {
        return IsSinking;
    }
    public void setIsSInking(bool value)
    {
        IsSinking = value;
    }

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSinking)
        {
            DestroyLilypad();
        }
    }

    public void DestroyLilypad()
    {
        Destroy(gameObject);
    }
}
