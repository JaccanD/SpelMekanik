using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    private static float health = 10;
    private static bool fire;

    public static float getHealth()
    {
        return health;
    }
    public static void setHealth(float value)
    {
        health = value;
    }
    public static void changeHealth(float value)
    {
        health += value;
    }
    public static bool getFire()
    {
        return fire;
    }
    public static void setFire(bool value)
    {
        fire = value;
    }
}
