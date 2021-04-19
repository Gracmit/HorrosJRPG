using System.Collections.Generic;
using UnityEngine;

public class CombatEnemy : ICombatEntity
{
    private CombatEnemyData _data;
    private ElementType _element = ElementType.None;
    private bool _alive = true;
    private GameObject _combatAvatar;
    private Dictionary<StatType, BuffCounter> _activeBuffs;
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

    public ElementType Element => _element;

    public void TakeDamage(int damage)
    {
        _data.Stats.Subtract(StatType.HP, damage);
        Debug.Log($"{_data.Name} took {damage} damage. {_data.Stats.GetValue(StatType.HP)} HP remaining");
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

    public void ChangeElement(ElementType element)
    {
        _element = element;
    }

    public void PrepareAttack()
    {
        BattleManager.Instance.AttackHandler.SaveAttack(ChooseAttack());
        BattleManager.Instance.AttackHandler.SaveTarget(ChooseTarget());
    }

    private Skill ChooseAttack()
    {
        
        var index = Random.Range(0, _data.Skills.Count);
        return _data.Skills[index];
    }

    private PartyMember ChooseTarget()
    {
        var index = Random.Range(0, BattleManager.Instance.PartyCount); 
        return BattleManager.Instance.Party[index];
    }

    public void Highlight()
    {
        Debug.Log("Hihlighted enemy:" + _data.Name);
        //1§ c_model.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
    }
    
    public void AddBuff(BuffSkillData buff)
    {
        if (_activeBuffs.ContainsKey(buff.Stat))
        {
            if (!_activeBuffs[buff.Stat].ModifyBuff(buff))
                _activeBuffs.Remove(buff.Stat);
        }
        else
        {
            _activeBuffs.Add(buff.Stat, new BuffCounter(buff.Multiplier, buff.Lenght));
        }
    }

    public void FullHeal()
    {
        _data.Stats.FullHeal();
    }
}