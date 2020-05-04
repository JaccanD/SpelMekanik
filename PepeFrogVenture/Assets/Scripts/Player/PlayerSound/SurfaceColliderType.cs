using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author: Jack Noaksson
public class SurfaceColliderType : MonoBehaviour
{

    public enum Mode { Default, Stock, Lily }
    public Mode TerrainType;


    public string GetTerrainType()
    {
        string typeString = "";

        switch (TerrainType)
        {
            case Mode.Default:
                typeString = "Default";
                break;
            case Mode.Stock:
                typeString = "Stock";
                break;
            case Mode.Lily:
                typeString = "Lily";
                break;
            default:
                typeString = "";
                break;
        }
        return typeString;
    }
}
