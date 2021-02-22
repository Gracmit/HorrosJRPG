using UnityEngine;

[CreateAssetMenu(fileName = "CombatEnemy", menuName = "CombatEntity/Enemy")]
public class CombatEnemy : ScriptableObject, ICombatEntity
{
    [SerializeField] private string _name;
    [SerializeField] private Stats _stats = new Stats();
    [SerializeField] private GameObject _model;
    private bool _attacked;
    private bool _attackChosen;
    public Object Model => _model;
    public bool Attacked => _attacked;
    public bool AttackChosen => _attackChosen;
    public bool Alive => true;

    public void TakeDamage()
    {
        Debug.Log("Damage Taken");
        if (_stats.Get(StatType.HP) <= 0)
        {
            Died();
        }
    }

    public void Died()
    {
        BattleManager.Instance.RemoveFromTurnQueue(this);
    }

    public void ChooseAttack()
    {
        _attackChosen = true;
    }

    public void ResetChosenAttack()
    {
        _attacked = false;
        _attackChosen = false;
    }

    public void Attack()
    {
        _attacked = true;
    }
}