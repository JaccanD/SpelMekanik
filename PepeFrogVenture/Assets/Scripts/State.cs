using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author: Jacob Didenbäck
public abstract class State : ScriptableObject
{
    public object owner;
    public StateMachine stateMachine;
    public virtual void Enter() { }
    public virtual void Run() { }
    public virtual void Exit() { } 
}
