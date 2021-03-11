using UnityEngine;

public interface IPlayerInput
{
    float Vertical { get; }
    float Horizontal { get; }
    bool GetKeyDown(KeyCode keyCode);
    bool GetButtonDown(string buttonName);
}