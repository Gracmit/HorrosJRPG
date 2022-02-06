#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffSkill", menuName = "Skill/BuffSkill")]
public class BuffSkill : Skill
{
    [SerializeField] private BuffSkillData _data;

    public override SkillData Data => _data;
    
    public BuffSkill(BuffSkillData data)
    {
        _data = data;
    }
    public override IEnumerator HandleAttack(ICombatEntity attacker, List<ICombatEntity> targets)
    {
        foreach (var target in targets)
        {
            target.AddBuff(_data);
            Instantiate(_data.Effect, target.CombatAvatar.GetComponentInChildren<FindTransform>().transform);
        }
        yield return null;
    }
    
    
#if UNITY_EDITOR
    private void Awake()
    {
        var fileName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(this));
        name = fileName;
    }
#endif
}