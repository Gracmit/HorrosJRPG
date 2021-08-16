using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _movementSpeed = 5;
    [SerializeField] private float turnSmoothTime = 0.1f;

    private CharacterController _controller;
    private float _turnSmoothVelocity;

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
        Vector3 direction = new Vector3(PlayerInput.Instance.Horizontal, 0f, PlayerInput.Instance.Vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * (_movementSpeed * Time.deltaTime));
        }
    }
}