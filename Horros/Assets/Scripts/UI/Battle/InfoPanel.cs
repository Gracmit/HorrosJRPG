using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _statusText;

    public void UpdateText(ICombatEntity entity)
    {
        _nameText.SetText(entity.Data.Name);
        _hpText.SetText($"HP: {entity.Data.Stats.GetValue(StatType.HP)}/{entity.Data.Stats.GetValue(StatType.MaxHP)}");
        _statusText.SetText($"Status: {entity.Element}");
    }
}