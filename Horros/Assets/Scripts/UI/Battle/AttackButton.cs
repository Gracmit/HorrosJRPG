using System.Collections;
using UnityEngine;


public class AttackButton : MonoBehaviour
{
    [SerializeField] private OffensiveSkill _skill;


    public void AttackChosen()
    {
        BattleManager.Instance.SaveChosenAttack(_skill);
        StartCoroutine(Highlight());
    }

    private IEnumerator Highlight()
    {
        yield return null;
        BattleUIManager.Instance.HighlightEnemy();
    }
}
