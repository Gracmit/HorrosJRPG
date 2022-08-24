using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class Cauldron : MonoBehaviour
{
    PlayableDirector _director;

    private void Awake()
    {
        _director = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (InputHandler.Instance.Controls.Player.Test.WasPressedThisFrame())
        {
            _director.Play();
        }
    }
}
