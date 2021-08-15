﻿using NSubstitute.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatusPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _mpText;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _mpSlider;
    [SerializeField] private Image _statusImage;
    [SerializeField] private Image _image;
    private PartyMember _partyMember;
    public PartyMember PartyMember => _partyMember;

    public void SetCharacter(PartyMember member)
    {
        _partyMember = member;
        _hpText.SetText(_partyMember.Data.Stats.GetValue(StatType.HP).ToString());
        _mpText.SetText(_partyMember.Data.Stats.GetValue(StatType.MP).ToString());
        _hpSlider.maxValue = _partyMember.Data.Stats.GetValue(StatType.MaxHP);
        _hpSlider.value = _partyMember.Data.Stats.GetValue(StatType.HP);
        _mpSlider.maxValue = _partyMember.Data.Stats.GetValue(StatType.MaxMP);
        _mpSlider.value = _partyMember.Data.Stats.GetValue(StatType.MP);
        _image.sprite = _partyMember.PartyMemberData.Portrait;
        _statusImage.sprite = _partyMember.Effect.Icon;
    }

    public void UpdatePanel()
    {
        _hpText.SetText(_partyMember.Data.Stats.GetValue(StatType.HP).ToString());
        _mpText.SetText(_partyMember.Data.Stats.GetValue(StatType.MP).ToString());
        _hpSlider.value = _partyMember.Data.Stats.GetValue(StatType.HP);
        _mpSlider.value = _partyMember.Data.Stats.GetValue(StatType.MP);
        _statusImage.sprite = _partyMember.Effect.Icon;
    }
}
