using UnityEngine;

public class GuardButton : MonoBehaviour
{
    [SerializeField] private Skill _skill;
    
    public void Guard()
    {
        BattleManager.Instance.SaveChosenAttack(_skill);
        BattleManager.Instance.ActiveMember.AttackHandler.SaveTargets(BattleManager.Instance.ActiveMember);
    }
}
