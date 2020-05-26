using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTexture : MonoBehaviour
{
    public float MoveX = 0.02f;
    public float MoveY = 0.02f;
    // Update is called once per frame

    void Update()
    {
        float OffsetX = Time.time * MoveX;
        float OffsetY = Time.time * MoveY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, OffsetY);
    }
}
