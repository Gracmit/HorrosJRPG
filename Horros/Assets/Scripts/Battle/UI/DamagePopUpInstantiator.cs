using System.Collections;
using TMPro;
using UnityEngine;

public class DamagePopUpInstantiator : MonoBehaviour
{
    [SerializeField] private TMP_Text _textPrefab;
    [SerializeField] private Camera _camera;

    private static DamagePopUpInstantiator _instance;

    public static DamagePopUpInstantiator Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void InstantiatePopUp(ICombatEntity target, int damage)
    {
        var popup = Instantiate(_textPrefab, transform, true);
        popup.transform.position = _camera.WorldToScreenPoint(target.CombatAvatar.transform.position + new Vector3(0, 0, 0));
        popup.text = damage.ToString();
        StartCoroutine(AnimatePopUp(popup));
    }

    private IEnumerator AnimatePopUp(TMP_Text popup)
    {
        yield return new WaitForSeconds(1f);
        Destroy(popup);
    }
}