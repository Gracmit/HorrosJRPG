using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OffensiveSkill))]
public class OffensiveSkillEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var skillObject = (OffensiveSkill)target;
        base.OnInspectorGUI();

        var dataEditor = CreateEditor(serializedObject.FindProperty("_data").objectReferenceValue);
        if(dataEditor != null)
            dataEditor.OnInspectorGUI();

        if (GUILayout.Button("Create Skill Data"))
        {
            
            if(skillObject.Data == null)
            {
                var skillData = CreateInstance<OffensiveSkillData>();
                AssetDatabase.AddObjectToAsset(skillData, skillObject);
                skillData.name = "Data";
                
                var property = serializedObject.FindProperty("_data");
                property.objectReferenceValue = skillData;
                serializedObject.ApplyModifiedProperties();
                
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(skillData));
            }
        }
        
    }
}

[CustomEditor(typeof(BuffSkill))]
public class BuffSkillEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var skillObject = (BuffSkill) target;
        base.OnInspectorGUI();

        var dataEditor = CreateEditor(serializedObject.FindProperty("_data").objectReferenceValue);
        if (dataEditor != null)
            dataEditor.OnInspectorGUI();

        if (GUILayout.Button("Create Skill Data"))
        {

            if (skillObject.Data == null)
            {
                var skillData = CreateInstance<BuffSkillData>();
                AssetDatabase.AddObjectToAsset(skillData, skillObject);
                skillData.name = "Data";

                var property = serializedObject.FindProperty("_data");
                property.objectReferenceValue = skillData;
                serializedObject.ApplyModifiedProperties();

                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(skillData));
            }
        }
    }
}