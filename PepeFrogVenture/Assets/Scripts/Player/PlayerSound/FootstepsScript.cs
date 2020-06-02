using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
// Author: Jack Noaksson
public class FootstepsScript : MonoBehaviour
{

    [SerializeField] private AudioSource FootstepsSource; //Jack 
    [SerializeField] private LayerMask collisionMask;

    [Header("Sounds")]
    private double time; // Jack
    private float filterTime; // Jack
    private string colliderType; // Jack
    private CapsuleCollider collider;
    public ParticleSystem grassParticle;
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

        collider = GetComponent<CapsuleCollider>();
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

    //private void PlayDynamicFootstepSound() // Jack
    //{
    //    if (AudioSettings.dspTime < time + filterTime)
    //    {
    //        return;
    //    }

    //    switch (colliderType)
    //    {
    //        case "Stock":                                           //Detta funkar inte av någon anledning...
    //            FootstepsSource.PlayOneShot(stockSound);
    //            break;
    //        case "Lily":
    //            FootstepsSource.PlayOneShot(lilySound);
    //            break;
    //        default:
    //            FootstepsSource.PlayOneShot(defaultSound);
    //            break;
    //    }

    //    time = AudioSettings.dspTime;
    //}

    private void PlayStaticFootstepSound() // Jack
    {

        string groundTag = CheckGroundTag();
        Debug.Log(groundTag);
        if (AudioSettings.dspTime < time + filterTime)
        {
            return;
        }
        else
        {
            FootstepsSource.PlayOneShot(defaultSound);
        }
        time = AudioSettings.dspTime;
        if (groundTag == "Island")
        {
            grassParticle.Play();
            FootstepsSource.PlayOneShot(default);
        }
        if(groundTag == "Log")
        {
            FootstepsSource.PlayOneShot(stockSound);
        }
        if(groundTag == "Lilypad")
        {
            FootstepsSource.PlayOneShot(lilySound);
        }
    }
    private string CheckGroundTag()
    {
        Vector3 topPoint = transform.position + Vector3.up * (collider.height - collider.radius);
        Vector3 botPoint = transform.position + Vector3.up * collider.radius;

        Physics.CapsuleCast(topPoint, botPoint, collider.radius, Vector3.down, out RaycastHit cast, 5f, collisionMask);
        if(cast.collider != null)
        {
            return cast.collider.tag;
        }
        return "nothing";
    }
}
