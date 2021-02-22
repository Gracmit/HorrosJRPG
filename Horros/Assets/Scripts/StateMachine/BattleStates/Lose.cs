using System;
using UnityEngine;

public class Lose : IState
{
    public void Tick()
    {
        throw new NotImplementedException();
    }

    public void OnEnter()
    {
        Debug.Log("Lost");
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }
}