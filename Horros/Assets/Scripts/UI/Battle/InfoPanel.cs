using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _icon;

    public void UpdatePanel(ICombatEntity entity)
    {
        _nameText.SetText(entity.Data.Name);
        _icon.sprite = entity.Effect.Icon;
    }
}