using Cinemachine;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook _freeLook;
    private PlayerMovementController _playerMovementController;
    private bool _lockCamera;
    private static ControlsManager _instance;

    public static ControlsManager Instance => _instance;
    private void Awake()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
        _instance = this;
        _playerMovementController = FindObjectOfType<PlayerMovementController>();
    }

    public void FreezeTime(bool freeze) => Time.timeScale = freeze ? 0 : 1;

    public void FreezeMovement(bool freeze) => _playerMovementController.FreezeControls(freeze);

    public void LockCamera(bool locking) => _lockCamera = locking;

    public float GetAxisCustom(string axisName){
        if(axisName == "Mouse X")
        {
            if (!_lockCamera){
                return Input.GetAxis("Mouse X");
            }

            return 0;
        }

        if (axisName == "Mouse Y")
        {
            if (!_lockCamera){
                return Input.GetAxis("Mouse Y");
            }

            return 0;
        }
        
        return Input.GetAxis(axisName);
    }
}