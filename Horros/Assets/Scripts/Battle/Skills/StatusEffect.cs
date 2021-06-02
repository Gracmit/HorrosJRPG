using System;
using UnityEngine;

[Serializable]
public class StatusEffect
{
    [SerializeField] private EffectType _type;
    [SerializeField] private ElementType _effectElement;
    [SerializeField] private int _chance;
    public EffectType EffectType => _type;
    public int Chance => _chance;
    public ElementType Element => _effectElement;
}