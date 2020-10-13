using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemButton : MonoBehaviour
{
    public string ButtonText => transform.GetComponentInChildren<TextMeshProUGUI>().text;
    
    public void SetItem(Item item)
    {
        transform.GetComponentInChildren<TextMeshProUGUI>().text = item.name + " " + item.Amount;
    }
}