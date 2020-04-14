using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lilypads : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private bool IsSinking = false;
    [SerializeField] private float sinkingSpeed = 1.5f;
    [SerializeField] private float MinSinkDepth = -5f;
    [SerializeField] private float MaxRaiseHeight = 1f;
    [SerializeField] private float RaiseSpeed = 1.5f;
    // Start is called before the first frame update


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public bool getIsSinking()
    {
        return IsSinking;
    }
    public void setIsSInking(bool value)
    {
        IsSinking = value;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsSinking)
        {
            SinkLilypad();
        }
        else if(transform.position.y < MaxRaiseHeight)
        {
            RaiseLilypad();
        }
    }

    public void SinkLilypad()
    {
        transform.position += Vector3.down * sinkingSpeed * Time.deltaTime;
        if(transform.position.y < MinSinkDepth)
        {
            IsSinking = false;
        }
    }

    public void RaiseLilypad()
    {
        transform.position += Vector3.up * RaiseSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        if (other.gameObject == player)
        {
            Debug.Log("playerentered");
            IsSinking = true;
            player.transform.parent = transform;
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exiting");
        if (other.gameObject == player)
        {
            player.transform.parent = null;
            Debug.Log("Exiting player");
        }
    }
}