using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _price;
    private Button _button;
    private Item _item;

    public Item Item => _item;

    public void SetItem(Item itemData)
    {
        _item = itemData;
        _itemName.SetText(itemData.Name);
        _price.SetText(itemData.ItemData.BuyingPrice.ToString());
    }

    public void ShowInfo() => ShopPanel.Instance.ShowInfo(_item);

    public void Buy()
    {
        var inventory = FindObjectOfType<Inventory>();
        inventory.PickUpItem(_item);
        inventory.SpendMoney(_item.ItemData.BuyingPrice);
    }
}