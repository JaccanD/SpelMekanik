using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairScript : MonoBehaviour
{
    // Cameran
    // Tungländ

    [Header ("Required")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] float toungeLength;
    [SerializeField] LayerMask hookMask;
    Image crossHair;

    [Header ("Cross Hair Colors")]
    [SerializeField] Color missColor;
    [SerializeField] Color hitColor;

    private void Start()
    {
        crossHair = GetComponent<Image>();
        crossHair.color = missColor;
    }
    private void Update()
    {
        bool aimHit = Physics.SphereCast(cameraTransform.position, 0.3f, cameraTransform.rotation * new Vector3(0, 0, 1), out RaycastHit HookCast, toungeLength, hookMask);

        if (aimHit)
        {
            crossHair.color = hitColor;
        }
        else
        {
            crossHair.color = missColor;
        }
    }
}
