using System.IO;
using UnityEditor;

public class SkillModificationProsessor : UnityEditor.AssetModificationProcessor
{
    private static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
    {
        CheckOffensiveSkillMoveResult(sourcePath, destinationPath);
        CheckBuffSkillMoveResult(sourcePath, destinationPath);

        return AssetMoveResult.DidNotMove;
    }
    
    private static void CheckOffensiveSkillMoveResult(string sourcePath, string destinationPath)
    {
        var skill = AssetDatabase.LoadMainAssetAtPath(sourcePath) as OffensiveSkill;
        if (skill == null)
        {
            return;
        }

        var sourceDirectory = Path.GetDirectoryName(sourcePath);
        var destinationDirectory = Path.GetDirectoryName(destinationPath);

        if (sourceDirectory != destinationDirectory)
        {
            return;
        }

        var fileName = Path.GetFileNameWithoutExtension(destinationPath);
        skill.name = fileName;
    }
    
    private static void CheckBuffSkillMoveResult(string sourcePath, string destinationPath)
    {
        var skill = AssetDatabase.LoadMainAssetAtPath(sourcePath) as BuffSkill;
        if (skill == null)
        {
            return;
        }

        var sourceDirectory = Path.GetDirectoryName(sourcePath);
        var destinationDirectory = Path.GetDirectoryName(destinationPath);

        if (sourceDirectory != destinationDirectory)
        {
            return;
        }

        var fileName = Path.GetFileNameWithoutExtension(destinationPath);
        skill.name = fileName;
    }
}
