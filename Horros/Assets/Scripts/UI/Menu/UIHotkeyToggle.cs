using UnityEngine;

public class UIHotkeyToggle : MonoBehaviour
{
    [SerializeField] private KeyCode _keyCode = KeyCode.I;
    [SerializeField] private GameObject _gameObjectToToggle;
    private void Update()
    {
        if (PlayerInput.Instance.Controls.Player.OpenInventory.WasPressedThisFrame())
        {
            _gameObjectToToggle.SetActive(!_gameObjectToToggle.activeSelf);
        }
    }
}

