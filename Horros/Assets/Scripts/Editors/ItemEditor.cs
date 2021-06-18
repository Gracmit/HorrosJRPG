using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Consumable))]
public class ConsumableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var itemObject = (Consumable)target;
        base.OnInspectorGUI();

        var effectEditor = CreateEditor(serializedObject.FindProperty("_effect").objectReferenceValue);
        if(effectEditor != null)
            effectEditor.OnInspectorGUI();
        EditorGUILayout.HelpBox("Choose item type before creating effect", MessageType.Info);
    
        if (GUILayout.Button("Create Effect"))
        {
            if(itemObject.Effect == null)
            {
                switch (itemObject.Type)
                {
                    case ConsumableType.Heal:
                        AddEffectToItem(CreateInstance<HealSkill>(), itemObject);
                        break;
                    case ConsumableType.Revive:
                        AddEffectToItem(CreateInstance<ReviveSkill>(), itemObject);
                        break;
                    case ConsumableType.Offensive:
                        AddEffectToItem(CreateInstance<OffensiveSkill>(), itemObject);
                        break;
                    case ConsumableType.Buff:
                        AddEffectToItem(CreateInstance<BuffSkill>(), itemObject);
                        break;
                    default:
                        Debug.Log("Add this ConsumableType to correct enum");
                        break;
                }
            }
        }
    }

    private void AddEffectToItem(Skill skillToAdd, Consumable assetObject)
    {
        AssetDatabase.AddObjectToAsset(skillToAdd, assetObject);
        skillToAdd.name = "Effect";
        var skillProperty = serializedObject.FindProperty("_effect");
        skillProperty.objectReferenceValue = skillToAdd;
        serializedObject.ApplyModifiedProperties();
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(skillToAdd));
    }
}
