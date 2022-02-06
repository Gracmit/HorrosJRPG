using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField] private GameObject _itemButton;
    private readonly List<GameObject> _buttons = new List<GameObject>();
    
    public void InstantiateButtons(List<Item> items)
    {
        if (_buttons.Count > 0)
        {
            DeleteOldButtons();
        }

        foreach (var item in items)
        {
            var button = Instantiate(_itemButton);
            _buttons.Add(button);

            var skillButton = button.GetComponent<CombatItemButton>();
            skillButton.SetItem(item);
            button.transform.SetParent(transform);
        }
    }
    
    private void DeleteOldButtons()
    {
        for (int i = _buttons.Count - 1; i >= 0; i--)
        {
            var button = _buttons[i];
            _buttons.Remove(button);
            Destroy(button);
        }
    }
    
    private void OnEnable()
    {
        BattleUIManager.Instance.EventHandler.ActivateItemButton(_buttons[0]);
    }
}