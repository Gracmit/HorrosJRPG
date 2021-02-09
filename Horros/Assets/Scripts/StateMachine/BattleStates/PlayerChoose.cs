using System;
using UnityEngine;

public class PlayerChoose : IState
{
    public void Tick()
    {
        throw new NotImplementedException();
    }

    public void OnEnter()
    {
        Debug.Log("Changed to PlayerChoose state");
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }
}