using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _movementSpeed = 5;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        CameraRelativeMovement();
    }

    private void CameraRelativeMovement()
    {
        Vector3 camForward = _camera.transform.forward;
        Vector3 camRight = _camera.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized;
        camRight = camRight.normalized;
        var desiredDir = camForward * PlayerInput.Instance.Vertical + camRight * PlayerInput.Instance.Horizontal;
        _controller.SimpleMove(desiredDir * _movementSpeed);
        if (desiredDir.normalized.magnitude > 0.1f)
            transform.forward = desiredDir.normalized * 360;
    }
}