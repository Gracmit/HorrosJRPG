using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatItemButton : MonoBehaviour
{
    private Consumable _item;
    private TMP_Text _text;
    private Button _button;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _button = GetComponent<Button>();
    }
    
    public void SetItem(Item item)
    {
        _item = (Consumable)item;
        _text.SetText($"{_item.Name} : {_item.Amount}");
    }

    public void ItemChosen()
    {
        BattleManager.Instance.SaveChosenAttack(_item.Effect);
        StartCoroutine(Highlight());
    }
    
    private IEnumerator Highlight()
    {
        yield return null;
        BattleUIManager.Instance.HighlightEntity();
    }
}