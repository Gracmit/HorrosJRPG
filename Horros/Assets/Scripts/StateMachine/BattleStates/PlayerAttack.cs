using System;
using UnityEngine;

public class PlayerAttack : IState
{
    public void Tick()
    {
        throw new NotImplementedException();
    }

    public void OnEnter()
    {
        Debug.Log("Changed State to PlayerAttack");
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }
}