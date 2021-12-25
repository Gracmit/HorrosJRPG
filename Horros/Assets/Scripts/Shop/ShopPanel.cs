using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private ShopButton _shopButton;
    [SerializeField] private GameObject _list;
    [SerializeField] private ShopItemInfo _infoPanel;
    [SerializeField] private TMP_Text _moneyText;

    private static ShopPanel _instance;
    private Shop[] _shops;
    private List<ShopButton> _buttons = new List<ShopButton>();
    private Inventory _inventory;
    private CanvasGroup _canvasGroup;
    private PlayerMovementController _player;


    public static ShopPanel Instance => _instance;

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        _canvasGroup = GetComponent<CanvasGroup>();
        _shops = FindObjectsOfType<Shop>();
        _inventory = FindObjectOfType<Inventory>();
        _player = FindObjectOfType<PlayerMovementController>();
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;
    }

    private void OnEnable()
    {
        _inventory.MoneyChanged += HandleMoneyChanged;
        foreach (var shop in _shops)
        {
            shop.ShopOpened += OpenShop;
        }
        _moneyText.SetText(_inventory.MoneyAmount.ToString());
    }

    private void OnDisable()
    {
        _inventory.MoneyChanged -= HandleMoneyChanged;
        foreach (var shop in _shops)
        {
            shop.ShopOpened -= OpenShop;
        }
    }

    private void Update()
    {
        if (_canvasGroup.interactable && PlayerInput.Instance.GetKeyDown(KeyCode.Q))
            CloseShop();
    }

    private void CloseShop()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;

        _player.FreezeControls(false);
    }

    private void HandleMoneyChanged()
    {
        foreach (var button in _buttons)
        {
            if (button.Item.BuyingPrice > _inventory.MoneyAmount)
            {
                button.GetComponent<Button>().interactable = false;
            }
            else
            {
                button.GetComponent<Button>().interactable = true;
            }
        }

        _moneyText.SetText(_inventory.MoneyAmount.ToString());
    }

    private void OpenShop(List<Item> items)
    {
        _canvasGroup.interactable = true;
        _canvasGroup.alpha = 1;
        
        if (_buttons.Count > 0)
        {
            for (int i = _buttons.Count - 1; i >= 0; i--)
            {
                var button = _buttons[i];
                _buttons.Remove(button);
                Destroy(button.gameObject);
            }
        }
        
        foreach (var item in items)
        {
            CreateButton(item);
            HandleMoneyChanged();
        }
        
        _player.FreezeControls(true);
    }

    private void CreateButton(Item item)
    {
        var button = Instantiate(_shopButton, _list.transform);
        _buttons.Add(button);

        var shopButton = button.GetComponent<ShopButton>();
        shopButton.SetItem(item);
    }

    public void ShowInfo(Item item)
    {
        _infoPanel.SetText(item);
    }
}