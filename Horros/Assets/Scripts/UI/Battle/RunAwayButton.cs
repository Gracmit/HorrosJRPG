using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayButton : MonoBehaviour
{
    public void RunAway()
    {
        BattleManager.Instance.EscapeFromBattle();
    }
}
