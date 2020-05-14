using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerControlInAirState : PlayerControlBaseState
{
   [SerializeField] new protected float Acceleration;
    
    protected float AirResistance { get { return Player.AirResistance; } }

    // Air resistance
    protected void ApplyAirResistance()
    {
        Velocity *= Mathf.Pow(AirResistance, Time.deltaTime);
    }
}
