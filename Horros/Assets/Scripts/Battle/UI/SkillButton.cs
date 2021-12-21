using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    private Skill _skill;
    private TMP_Text _text;
    private Button _button;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _button = GetComponent<Button>();
    }

    public void SetSkill(Skill skill)
    {
        _skill = skill;
        _text.SetText(skill.Data.Name);
        var remainingMP = BattleManager.Instance.ActiveMember.Data.Stats.GetValue(StatType.MP);
        if (remainingMP < _skill.Data.MpCost)
        {
            _button.interactable = false;
        }
    }

    public void AttackChosen()
    {
        BattleManager.Instance.SaveChosenAttack(_skill);
        if (_skill.Data.MultiAttack)
            StartCoroutine(HighlightAll());
        else
            StartCoroutine(Highlight());
    }

    private IEnumerator HighlightAll()
    {
        yield return null;
        BattleUIManager.Instance.HighlightAll();
    }

    private IEnumerator Highlight()
    {
        yield return null;
        BattleUIManager.Instance.HighlightEntity();
    }
    
    
}