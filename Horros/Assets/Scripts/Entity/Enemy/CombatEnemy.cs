using UnityEngine;

public class CombatEnemy : ICombatEntity
{
    private CombatEnemyData _data;
    private bool _alive = true;
    private GameObject _combatAvatar;
    public GameObject Model => _data.Model;    
    public bool Alive => _alive;
    public EntityData Data => _data;
    public GameObject CombatAvatar
    {
        
        get => _combatAvatar;
        set => _combatAvatar = value;
    }

    public CombatEnemy(CombatEnemyData data)
    {
        _data = data;
    }
    
    public void TakeDamage()
    {
        _data.Stats.Remove(StatType.HP, 10);
        Debug.Log($"{_data.Name} took 10 damage. {_data.Stats.GetValue(StatType.HP)} HP remaining");
        if (_data.Stats.GetValue(StatType.HP) <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _alive = false;
        BattleManager.Instance.RemoveFromTurnQueue(this);
        GameObject.Destroy(_combatAvatar);
        
    }

    public void ChooseAttack()
    {
        BattleManager.Instance.AttackHandler.SaveAttack();
        BattleManager.Instance.AttackHandler.SaveTarget(ChooseTarget());
    }

    private PartyMember ChooseTarget()
    {
        return BattleManager.Instance.Party[0];
    }

    public void Highlight()
    {
        Debug.Log("Hihlighted enemy:" + _data.Name);
        //1§ c_model.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
    }

    public void FullHeal()
    {
        _data.Stats.FullHeal();
    }
}