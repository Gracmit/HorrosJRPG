using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] QuestPanel _questPanel;

    List<Quest> _activeQuests = new List<Quest>();
    static QuestManager _instance;

    public static QuestManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
            return;

        _instance = this;
    }

    public void AddQuest(Quest quest)
    {
        _activeQuests.Add(quest);
        _questPanel.SelectQuest(quest);
    }
    
    [ContextMenu("Progress Quest")]
    public void ProgressQuests()
    {
        foreach (var quest in _activeQuests)
        {
            quest.TryProgress();
        }
    }
}