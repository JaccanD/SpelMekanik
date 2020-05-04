using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author: Jack Noaksson
public class FootstepsScript : MonoBehaviour
{

    [SerializeField] private AudioSource FootstepsSource; //Jack 


    [Header("Sounds")]
    private double time; // Jack
    private float filterTime; // Jack
    private string colliderType; // Jack
    public AudioClip defaultSound; // Jack                      //Detta är bara betan tills animationerna är klara
    public AudioClip stockSound; // Jack
    public AudioClip lilySound; // Jack


    // Start is called before the first frame update
    void Start()
    {
        FootstepsSource = GetComponent<AudioSource>();
        time = AudioSettings.dspTime; // Jack
        filterTime = 0.2f;
        FootstepsSource.volume = 0.12f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col) // Jack
    {
        SurfaceColliderType act = col.gameObject.GetComponent<Collider>().gameObject.GetComponent<SurfaceColliderType>();

        if (act)
        {
            colliderType = act.GetTerrainType();                            //Detta är bara betan tills animationerna är klara
        }
        Debug.Log("marken är av sorten: " + colliderType);

    }

    private void PlayDynamicFootstepSound() // Jack
    {
        if (AudioSettings.dspTime < time + filterTime)
        {
            return;
        }

        switch (colliderType)
        {
            case "Stock":                                           //Detta funkar inte av någon anledning...
                FootstepsSource.PlayOneShot(stockSound);
                break;
            case "Lily":
                FootstepsSource.PlayOneShot(lilySound);
                break;
            default:
                FootstepsSource.PlayOneShot(defaultSound);
                break;
        }

        time = AudioSettings.dspTime;
    }
}
