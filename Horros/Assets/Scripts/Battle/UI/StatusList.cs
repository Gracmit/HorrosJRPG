using System.Collections.Generic;
using UnityEngine;

internal class StatusList : MonoBehaviour
{
    [SerializeField] private List<StatusPanel> _statusPanels;

    public void InstantiateStatusPanel(PartyMember member)
    {
        foreach (var panel in _statusPanels)
        {
            if (panel.PartyMember == null)
            {
                panel.gameObject.SetActive(true);
                panel.SetCharacter(member);
                return;
            }
        }
    }

    public void UpdatePanel(PartyMember member)
    {
        _statusPanels.Find(x => x.PartyMember == member).UpdatePanel();
    }
}