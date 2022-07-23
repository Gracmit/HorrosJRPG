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
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
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
        if (_canvasGroup.interactable && InputHandler.Instance.Controls.UI.Cancel.WasPressedThisFrame())
            CloseShop();
    }

    private void CloseShop()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;

        ControlsManager.Instance.FreezeTime(false);
        ControlsManager.Instance.FreezeMovement(false);
    }

    private void HandleMoneyChanged()
    {
        foreach (var button in _buttons)
        {
            if (button.Item.ItemData.BuyingPrice > _inventory.MoneyAmount)
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
        _canvasGroup.blocksRaycasts = true;
        
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
        
        ControlsManager.Instance.FreezeTime(true);
        ControlsManager.Instance.FreezeMovement(true);
    }

    private void CreateButton(Item itemData)
    {
        var button = Instantiate(_shopButton, _list.transform);
        _buttons.Add(button);

        var shopButton = button.GetComponent<ShopButton>();
        shopButton.SetItem(itemData);
    }

    public void ShowInfo(Item itemData)
    {
        _infoPanel.SetText(itemData);
    }
}