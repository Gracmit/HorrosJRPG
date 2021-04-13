#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

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
    public override void HandleAttack(ICombatEntity attacker, ICombatEntity target)
    {
        target.AddBuff(_data);
    }
    
    
#if UNITY_EDITOR
    private void Awake()
    {
        var fileName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(this));
        name = fileName;
    }
#endif
}