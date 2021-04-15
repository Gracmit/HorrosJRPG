using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatusPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _mpText;
    [SerializeField] private TMP_Text _statusText;
    [SerializeField] private Image _image;
    private PartyMember _partyMember;
    public PartyMember PartyMember => _partyMember;

    public void SetCharacter(PartyMember member)
    {
        _partyMember = member;
        _nameText.SetText(_partyMember.Data.Name);
        _hpText.SetText($"HP: {_partyMember.Data.Stats.GetValue(StatType.HP)}/{_partyMember.Data.Stats.GetValue(StatType.MaxHP)}");
        _mpText.SetText($"MP: {_partyMember.Data.Stats.GetValue(StatType.MP)}/{_partyMember.Data.Stats.GetValue(StatType.MaxMP)}");
        _statusText.SetText($"Status: {_partyMember.Element.ToString()}");
    }

    public void UpdatePanel()
    {
        _hpText.SetText($"HP: {_partyMember.Data.Stats.GetValue(StatType.HP)}/{_partyMember.Data.Stats.GetValue(StatType.MaxHP)}");
        _mpText.SetText($"MP: {_partyMember.Data.Stats.GetValue(StatType.MP)}/{_partyMember.Data.Stats.GetValue(StatType.MaxMP)}");
        _statusText.SetText($"Status: {_partyMember.Element.ToString()}");
    }
}
