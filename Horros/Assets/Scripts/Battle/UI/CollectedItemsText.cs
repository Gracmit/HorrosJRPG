using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class CollectedItemsText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    public void SetText(List<Item> items)
    {
        StringBuilder text = new StringBuilder();

        foreach (var item in items)
        {
            text.AppendLine(item.Name);
        }
        
        _text.SetText(text);
    }
}

