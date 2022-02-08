using System.Collections;
using Cinemachine;
using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    [SerializeField] CinemachineSmoothPath[] _paths;
    [SerializeField] private Transform[] _lookAtTransforms;
    [SerializeField] CinemachineDollyCart _cart;

    CinemachineVirtualCamera _camera;
    int _index = 0;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    IEnumerator ChangeTrack()
    {
        yield return new WaitForSeconds(1.92f);

        if (_index + 1 > _paths.Length)
        {
            BattleCameraManager.Instance.ToggleIntroDone(true);
            _index = 0;
        }
        else
        {
            _camera.m_LookAt = _lookAtTransforms[_index];
            _cart.m_Path = _paths[_index];
            _cart.m_Position = 0;
            _index++;
            StartCoroutine(ChangeTrack());
        }
    }

    public void StartIntro()
    {
        _cart.m_Path = _paths[_index];
        _camera.m_LookAt = _lookAtTransforms[_index];
        _index++;
        StartCoroutine(ChangeTrack());
    }

}
