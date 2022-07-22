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
    private static readonly int Speed = Animator.StringToHash("Speed");


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_frozen)
        {
            _animator.SetFloat(Speed, 0);
            return;
        }
        
        CameraRelativeMovement();
    }

    private void CameraRelativeMovement()
    {

        Vector2 input = InputHandler.Instance.Controls.Player.Move.ReadValue<Vector2>().normalized;
        Vector3 direction = new Vector3(input.x, 0, input.y);
        var gravity = 2f;
        if (_controller.isGrounded) gravity = 0f;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            _controller.Move(moveDir.normalized * (_movementSpeed * Time.deltaTime) + new Vector3(0, -gravity, 0));
        }
        _animator.SetFloat(Speed, direction.magnitude);
    }

    public void FreezeControls(bool freeze) => _frozen = freeze;
}