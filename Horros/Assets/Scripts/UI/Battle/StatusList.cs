using UnityEngine;

internal class StatusList : MonoBehaviour
{
    [SerializeField] private StatusPanel _statusPanel;

    public void InstantiateStatusPanel(PartyMember member)
    {
        var panel = Instantiate(_statusPanel, transform);
        panel.SetCharacter(member);
    }
}