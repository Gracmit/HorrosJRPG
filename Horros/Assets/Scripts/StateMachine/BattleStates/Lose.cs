using System;
using UnityEngine;

public class Lose : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        Debug.Log("Lost");
    }

    public void OnExit()
    {
        
    }
}