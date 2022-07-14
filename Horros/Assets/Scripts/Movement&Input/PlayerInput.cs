using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    public static IPlayerInput Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        LockAndHideCursor();
    }
    
    public float Vertical => Input.GetAxisRaw("Vertical");
    public float Horizontal => Input.GetAxisRaw("Horizontal");

    public bool GetKeyDown(KeyCode keyCode) => Input.GetKeyDown(keyCode);

    public bool GetButtonDown(string buttonName) => Input.GetButtonDown(buttonName);

    public void LockAndHideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}