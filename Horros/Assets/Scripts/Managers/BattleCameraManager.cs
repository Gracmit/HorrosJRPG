using UnityEngine;
using Cinemachine;

public class BattleCameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _overviewCamera;
    private CinemachineVirtualCamera _activeCamera;
    private static BattleCameraManager _instance;

    public static BattleCameraManager Instance => _instance;

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

        _activeCamera = _overviewCamera;
    }

    public void SetCamera(CinemachineVirtualCamera cameraToActivate)
    {
        _activeCamera.gameObject.SetActive(false);
        _activeCamera = cameraToActivate;
        _activeCamera.gameObject.SetActive(true);
        
    }

    public void SetOverviewCamera()
    {
        _activeCamera.gameObject.SetActive(false);
        _activeCamera = _overviewCamera;
        _activeCamera.gameObject.SetActive(true);        
    }
}
