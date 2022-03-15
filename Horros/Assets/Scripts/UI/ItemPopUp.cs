using System.Collections;
using TMPro;
using UnityEngine;

public class ItemPopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _texts;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator UpdateUI(Item[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            var text = _texts[i];
            text.SetText($"{items[i].Name} : {items[i].Amount}");
        }

        yield return new WaitForSeconds(2);

        for (int i = 0; i < _texts.Length; i++)
        {
            _texts[i].SetText("");
        }
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void ShowItems(Item[] items)
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
        StartCoroutine(UpdateUI(items));

    }
}