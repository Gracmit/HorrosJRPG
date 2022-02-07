using TMPro;
using UnityEngine;

public class AttackText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    public void EnableText(string attackName)
    {
        _text.gameObject.SetActive(true);
        _text.SetText(attackName);
    }

    public void DisableText() => _text.gameObject.SetActive(false);
}
