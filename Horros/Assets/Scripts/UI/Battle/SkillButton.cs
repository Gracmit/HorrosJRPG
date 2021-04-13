using TMPro;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    private Skill _skill;
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    public void SetSkill(Skill skill)
    {
        _skill = skill;
        _text.SetText(skill.Data.Name);
    }

    public void AttackChosen()
    {
        BattleUIManager.Instance.HighlightEnemy();
        BattleManager.Instance.SaveChosenAttack(_skill);
    }
}