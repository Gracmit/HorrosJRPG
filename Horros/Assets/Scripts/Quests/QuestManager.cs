using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<Quest> _activeQuests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        _activeQuests.Add(quest);
    }
}
