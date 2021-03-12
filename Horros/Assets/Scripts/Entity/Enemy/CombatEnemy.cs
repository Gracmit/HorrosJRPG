using UnityEngine;

public class CombatEnemy : ICombatEntity
{
    private CombatEnemyData _data;
    private bool _attacked;
    private bool _attackChosen;
    public GameObject Model => _data.Model;
    public bool Attacked => _attacked;
    public bool AttackChosen => _attackChosen;
    public bool Alive => true;

    public CombatEnemy(CombatEnemyData data)
    {
        _data = data;
    }
    
    public void TakeDamage()
    {
        Debug.Log("Damage Taken");
        if (_data.Stats.Get(StatType.HP) <= 0)
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

    public void Highlight()
    {
        Debug.Log("Hihlighted enemy:" + _data.Name);
        //1§ c_model.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
    }
}