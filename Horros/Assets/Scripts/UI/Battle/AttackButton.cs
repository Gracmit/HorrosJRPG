using UnityEngine;


public class AttackButton : MonoBehaviour
{
    [SerializeField] private Skill _skill;
    
    public void AttackChosen()
    {
        BattleUIManager.Instance.HighlightEnemy();
        BattleManager.Instance.SaveChosenAttack(_skill);
    }
}
