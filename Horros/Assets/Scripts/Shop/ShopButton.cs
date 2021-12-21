using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _price;
    private Button _button;
    private Item _item;

    public void SetItem(Item item)
    {
        _item = item;
        _itemName.SetText(item.Name);
        _price.SetText(item.BuyingPrice.ToString());
    }

    public void ShowInfo() => ShopPanel.Instance.ShowInfo(_item);
}