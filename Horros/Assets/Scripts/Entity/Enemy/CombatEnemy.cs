using UnityEngine;

public class CombatEnemy : ICombatEntity
{
    private CombatEnemyData _data;
    public GameObject Model => _data.Model;
    public bool Alive => true;
    public EntityData Data => _data;

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
}