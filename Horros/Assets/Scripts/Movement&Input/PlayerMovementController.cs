using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _movementSpeed = 5;
    [SerializeField] private float turnSmoothTime = 0.1f;

    private CharacterController _controller;
    private Animator _animator;
    private float _turnSmoothVelocity;
    private bool _frozen;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_frozen)
            return;
        
        CameraRelativeMovement();
    }

    private void CameraRelativeMovement()
    {
        Vector3 direction = new Vector3(PlayerInput.Instance.Horizontal, 0f, PlayerInput.Instance.Vertical).normalized;
        var gravity = 2f;
        if (_controller.isGrounded) gravity = 0f;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            //Vector3 moveDir = new Vector3(transform.forward.x, _controller.velocity.y, transform.forward.z);

            //_controller.velocity = moveDir.normalized * _movementSpeed;
            _controller.Move(moveDir.normalized * (_movementSpeed * Time.deltaTime) + new Vector3(0, -gravity, 0));
        }
        _animator.SetFloat("Speed", direction.magnitude);
    }

    public void FreezeControls(bool freeze)
    {
        _frozen = freeze;
        if (freeze)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}