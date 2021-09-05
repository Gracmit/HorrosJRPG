using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatEnemy : ICombatEntity
{
    private CombatEnemyData _data;
    private StatusEffect _statusEffect;
    private bool _alive = true;
    private GameObject _combatAvatar;
    private Dictionary<StatType, BuffCounter> _activeBuffs = new Dictionary<StatType, BuffCounter>();
    private AttackHandler _attackHandler;
    private SkinnedMeshRenderer _renderer;
    public GameObject Model => _data.Model;
    public bool Alive => _alive;
    public EntityData Data => _data;
    public CombatEnemyData EnemyData => _data;

    public GameObject CombatAvatar
    {
        get => _combatAvatar;
        set => _combatAvatar = value;
    }

    public CombatEnemy(CombatEnemyData data)
    {
        _data = data;
        _combatAvatar = _data.Model;
    }

    public ElementType Element
    {
        get
        {
            if (_statusEffect == null)
            {
                return ElementType.None;
            }
            return _statusEffect.Element;
        }
    }
    
    public StatusEffect Effect => _statusEffect ?? new StatusEffect(BattleUIManager.Instance.GetNoneStatusIcon());
    public bool Attacked => _attackHandler.Attacked;
    public AttackHandler AttackHandler => _attackHandler;

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
        BattleManager.Instance.EnemyDied(this);
        Object.Destroy(_combatAvatar);
    }

    public void ChangeElement(StatusEffect effect)
    {
        _statusEffect = effect;
    }

    public void PrepareAttack()
    {
        List<PartyMember> party = new List<PartyMember>(BattleManager.Instance.Party.Where(x => x.Alive).ToList());
        List<CombatEnemy> enemies = new List<CombatEnemy>(BattleManager.Instance.Enemies);
        enemies.Remove(this);
        _data.AI.ChooseAction(this, party, enemies);
        _attackHandler.SaveAttack(_data.AI.GetSkill());
        _attackHandler.SaveTargets(_data.AI.GetTarget());
    }

    public void Highlight()
    {
        HighlightHealthBarInstantiator.Instance.ShowHealtBar(this);
    }

    public void UnHighlight()
    {
        HighlightHealthBarInstantiator.Instance.HideHealthBar();
    }

    public int GetStatValue(StatType type)
    {
        var statValue = _data.Stats.GetValue(type);
        if (_activeBuffs.ContainsKey(type))
            return (int) Mathf.Ceil(_activeBuffs[type].Multiplier * statValue);
        
        return statValue;
    }

    public void CheckBuffs()
    {
        foreach (var keyValuePair in _activeBuffs)
        {
            keyValuePair.Value.DecreaseRemainingTime();
            if (keyValuePair.Value.RemainingTime <= 0)
                _activeBuffs.Remove(keyValuePair.Key);
        }
    }

    public void AddBuff(BuffSkillData buff)
    {
        for(int i = 0; i < buff.Stat.Count; i++)
        {
            if (_activeBuffs.ContainsKey(buff.Stat[i]))
            {
                if (!_activeBuffs[buff.Stat[i]].ModifyBuff(buff))
                    _activeBuffs.Remove(buff.Stat[i]);
            }
            else
            {
                _activeBuffs.Add(buff.Stat[i], new BuffCounter(buff.Multiplier, buff.Lenght));
            }
            
        }
    }

    public void Attack()
    {
        if (_alive)
            _attackHandler.Attack();
        else
            _attackHandler.ToggleAttacked();
    }

    public void SetAttackHandler()
    {
        _combatAvatar.AddComponent<AttackHandler>();
        _attackHandler = _combatAvatar.GetComponent<AttackHandler>();
        _attackHandler.SetAttacker(this);
    }

    public void ResetAttack()
    {
        _attackHandler.ResetAttack();
    }

    public void FullHeal()
    {
        _data.Stats.FullHeal();
    }

    public void SetRenderer()
    {
        _renderer = _combatAvatar.GetComponentInChildren<SkinnedMeshRenderer>();
    }
}