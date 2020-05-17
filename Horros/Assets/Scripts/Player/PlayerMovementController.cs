using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 movementInput = new Vector3(PlayerInput.Instance.Horizontal * movementSpeed, 0, PlayerInput.Instance.Vertical * movementSpeed);
        Vector3 movement = transform.rotation * movementInput;
        _controller.SimpleMove(movement);
    }
}