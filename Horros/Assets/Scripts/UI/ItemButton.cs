using TMPro;
using UnityEngine;


public class ItemButton : MonoBehaviour
{
    private Item _item;
    public string ButtonText => transform.GetComponentInChildren<TextMeshProUGUI>().text;
    
    public void SetItem(Item item)
    {
        transform.GetComponentInChildren<TextMeshProUGUI>().text = item.name + " " + item.Amount;
        _item = item;
    }
    
}