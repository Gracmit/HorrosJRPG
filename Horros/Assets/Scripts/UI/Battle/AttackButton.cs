using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public void AttackChosen()
    {
        BattleUIManager.Instance.HighlightEnemy();
        //BattleManager.Instance.SaveChosenAttack();
    }
}
