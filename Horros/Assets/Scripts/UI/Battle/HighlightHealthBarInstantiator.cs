using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightHealthBarInstantiator : MonoBehaviour
{
    [SerializeField] private Slider _barPrefab;
    [SerializeField] private Camera _camera;
    private List<Slider> _healthBars = new List<Slider>();
    private List<GameObject> _targets = new List<GameObject>();

    private static HighlightHealthBarInstantiator _instance;

    public static HighlightHealthBarInstantiator Instance => _instance;

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

    private void Update()
    {
        for (int i = 0; i < _healthBars.Count; i++)
        {
            _healthBars[i].transform.position =
                _camera.WorldToScreenPoint(_targets[i].transform.position + new Vector3(0, 1, 0));
        }
    }

    public void ShowHealtBar(ICombatEntity target)
    {
        var healthBar = Instantiate(_barPrefab, transform, true);
        healthBar.transform.position = _camera.WorldToScreenPoint(target.CombatAvatar.transform.position + new Vector3(0, 1, 0));
        healthBar.maxValue = target.Data.Stats.GetValue(StatType.MaxHP);
        healthBar.value = target.Data.Stats.GetValue(StatType.HP);
        _healthBars.Add(healthBar);
        _targets.Add(target.CombatAvatar);
    }

    public void HideHealthBar()
    {
        foreach (var healthBar in _healthBars)
        {
            Destroy(healthBar.gameObject);
        }
        _healthBars.Clear();
        _targets.Clear();
    }

    public void InstantiatePopUpHealthBar(ICombatEntity target, int damage)
    {
        var popup = Instantiate(_barPrefab, transform, true);
        popup.transform.position = _camera.WorldToScreenPoint(target.CombatAvatar.transform.position + new Vector3(0, 1, 0));
        popup.maxValue = target.Data.Stats.GetValue(StatType.MaxHP);
        popup.value = target.Data.Stats.GetValue(StatType.HP);
        StartCoroutine(AnimatePopUp(popup, damage));
    }

    private IEnumerator AnimatePopUp(Slider healthBar, int damage)
    {
        yield return new WaitForSeconds(0.2f);
        healthBar.value = healthBar.value - damage;
        yield return new WaitForSeconds(1f);
        Destroy(healthBar.gameObject);
    }
}