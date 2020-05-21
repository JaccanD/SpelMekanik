using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  Statisk dictionary som innehåller alla keybindings
//  scripts kollar med GetKeyBinding vilken Keycode som 
//  en function ska användas på.
//  Används för att implementera rebindable keys.
//
public static class Controlls
{
    /// <summary>
    /// 
    /// </summary>
    private static Dictionary<Function, KeyCode> keyBindings = new Dictionary<Function, KeyCode>
        {
            { Function.ShootTounge, KeyCode.Mouse0 },
            { Function.ShootFireball, KeyCode.Mouse1 },
            { Function.Jump, KeyCode.Space },
            { Function.Interact, KeyCode.E },
            { Function.OpenMenu, KeyCode.Escape }
        };

    private static Dictionary<Function, KeyCode> controllerBindings = new Dictionary<Function, KeyCode>
        {
            { Function.ShootTounge, KeyCode.Joystick1Button4 }, // Left Bumber
            { Function.ShootFireball, KeyCode.Joystick1Button5 }, // Right Bumber
            { Function.Jump, KeyCode.Joystick1Button0 }, // A (Xbox)  X (Dualshock)
            { Function.Interact, KeyCode.Joystick1Button2 }, // X (Xbox) Square (Dualshock)
            { Function.OpenMenu, KeyCode.Joystick1Button7 } // Start
        };

    public static bool UsingController { get; set; } = false;
    public static Dictionary<Function, KeyCode> CurrentKeyMap { get { return UsingController == true ? controllerBindings : keyBindings; } }
    /// <summary>
    /// Returns the Keycode the function is bound too
    /// </summary>
    /// <param name="function"></param>
    /// <returns></returns>
    
    public static KeyCode GetKeyBinding(Function function)
    {
        return CurrentKeyMap[function];
    }

    //TODO
    // SetKeybinding
    // kontrollera först om en annan function redan är bindad till samma tangent
    // ge error eller unbinda den andra functionen, antagligen error
}
/// <summary>
/// All Player Actions
/// </summary>
public enum Function
{
    ShootTounge,
    ShootFireball,
    Jump,
    Interact,
    OpenMenu
}
