using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine
{
    private State currentState;
    private State nextState;

    private Dictionary<Type, State> StateDictionary = new Dictionary<Type, State>();

    public StateMachine(object controller, State[] states)
    {
        foreach(State state in states)
        {
            State instance = UnityEngine.Object.Instantiate(state);
            instance.owner = controller;
            instance.stateMachine = this;
            StateDictionary.Add(instance.GetType(), instance);

            if(nextState == null)
            {
                nextState = instance;
            }
        }
        currentState?.Enter();
    }
    public void TransitionTo<T>() where T : State
    {
        nextState = StateDictionary[typeof(T)];
    }
    private void UpdateState()
    {
        if(nextState != currentState)
        {
            currentState?.Exit();
            currentState = nextState;
            currentState?.Enter();
        }
    }
    public void Run()
    {
        UpdateState();
        currentState.Run();
    }
}
