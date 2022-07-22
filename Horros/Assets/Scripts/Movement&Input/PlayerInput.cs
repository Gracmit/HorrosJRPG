using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; set; }
    private Inputs _playerControls;
    private void Awake()
    {
        Instance = this;
        LockAndHideCursor();
        _playerControls = new Inputs();
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
    
    //public float Vertical => Input.GetAxisRaw("Vertical");
    //public float Horizontal => Input.GetAxisRaw("Horizontal");

    public bool GetKeyDown(KeyCode keyCode) => Input.GetKeyDown(keyCode);

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