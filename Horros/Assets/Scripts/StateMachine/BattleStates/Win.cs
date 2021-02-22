using System;
using UnityEngine;

public class Win : IState
{
    public void Tick()
    {
        throw new NotImplementedException();
    }

    public void OnEnter()
    {
        Debug.Log("Won");
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }
}