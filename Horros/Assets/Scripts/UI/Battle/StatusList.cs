using System.Collections.Generic;
using UnityEngine;

internal class StatusList : MonoBehaviour
{
    [SerializeField] private StatusPanel _statusPanel;
    private List<StatusPanel> _panels = new List<StatusPanel>();

    public void InstantiateStatusPanel(PartyMember member)
    {
        var panel = Instantiate(_statusPanel, transform);
        panel.SetCharacter(member);
        _panels.Add(panel);
    }

    public void UpdatePanel(PartyMember member)
    {
        _panels.Find(x => x.PartyMember == member).UpdatePanel();
    }
}