using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }
    private Inputs _playerControls;
    private PlayerInput _playerInput;
    private void Awake()
    {
        Instance = this;
        _playerControls = new Inputs();
        LockAndHideCursor();
        _playerInput = GetComponent<PlayerInput>();
    }
    
    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public Inputs Controls => _playerControls;

    public static void LockAndHideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    
    public static void UseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}