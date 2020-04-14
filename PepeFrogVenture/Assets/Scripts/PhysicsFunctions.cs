using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicsFunctions
{
    public static Vector2 NormalForce(Vector2 velocity, Vector2 normal)
    {
        Vector2 projection;
        float DotProdukt = Vector2.Dot(normal, velocity);
        if(DotProdukt >= 0.0f)
        {
            DotProdukt = 0.0f;
        }
        projection = DotProdukt * normal;
        return -projection;
    }

    public static Vector3 NormalForce(Vector3 velocity, Vector3 normal)
    {
        Vector3 projection;
        float DotProdukt = Vector3.Dot(velocity, normal);
        if (DotProdukt >= 0.0f)
        {
            DotProdukt = 0.0f;
        }
        projection = DotProdukt * normal;
        return -projection;
    }
}
