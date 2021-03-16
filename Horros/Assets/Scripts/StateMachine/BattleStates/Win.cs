using System;
using UnityEngine;

public class Win : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        Debug.Log("Won");
    }

    public void OnExit()
    {
    }
}