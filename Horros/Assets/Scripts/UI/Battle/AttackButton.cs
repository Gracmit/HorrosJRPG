using UnityEngine;


public class AttackButton : MonoBehaviour
{
    [SerializeField] private OffensiveSkill _skill;


    public void AttackChosen()
    {
        BattleUIManager.Instance.HighlightEnemy();
        BattleManager.Instance.SaveChosenAttack(_skill);
    }
}
