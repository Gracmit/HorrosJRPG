using TMPro;
using UnityEngine;


public class ItemButton : MonoBehaviour
{
    public void SetItem(Item item)
    {
        transform.GetComponentInChildren<TextMeshProUGUI>().text = item.name + " " + item.Amount;
    }
}