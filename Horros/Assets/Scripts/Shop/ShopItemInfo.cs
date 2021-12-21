using TMPro;
using UnityEngine;

public class ShopItemInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void SetText(Item item)
    {
        _text.SetText(item.Description);
    }
}
