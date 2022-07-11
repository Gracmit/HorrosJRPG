
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Bool Game Flag")]
public class GameFlag : ScriptableObject

{
    public static event Action AnyChanged;
    public bool Value { get; private set; }

    void OnEnable() => Value = default;

    public void Set(bool value)
    {
        Value = value;
        AnyChanged?.Invoke();
    }
}
