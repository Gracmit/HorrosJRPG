﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public void AttackChosen()
    {
        BattleManager.Instance.SaveChosenAttack();
    }
}
