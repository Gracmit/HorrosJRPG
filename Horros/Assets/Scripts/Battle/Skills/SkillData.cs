﻿using System;
using UnityEngine;

[Serializable]
public class SkillData : ScriptableObject
{
    [SerializeField] private string _name;
    [TextArea] [SerializeField] private string _description;
    [SerializeField] private int _mpCost;
    [SerializeField] private bool _multiAttack;

    public string Name => _name;
    public int MpCost => _mpCost;
    public bool MultiAttack => _multiAttack;
}