using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author: Valter Falsterljung
public static class PlayerStats
{
    private static float maxHealth = 10;
    private static float health = 10;

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
    public static void ResetHealth()
    {
        health = maxHealth;
    }
}
