using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _controller;
    private PlayerInputHandler _inputHandler;
    private PlayerLook _playerLook;

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravityConstant = -9.81f;

    [Header("Rotation")]
    [Tooltip("Degrees per second the player turns toward the input direction.")]
    [SerializeField] private float rotationSpeed = 720f;
    [SerializeField] private float inputDeadzone = 0.1f;

    private Vector3 _velocity;

    public Action OnMovement;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputHandler = GetComponent<PlayerInputHandler>();
        _playerLook = GetComponent<PlayerLook>();

        if (_playerLook == null)
            Debug.LogError("PlayerMove: PlayerLook missing on this object.", this);
    }

    private void Update()
    {
        UpdateWasd();
        UpdateJumpPressed();
        ApplyGravity();
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void UpdateWasd()
    {
        Vector2 input = _inputHandler.MoveInput;
        bool hasInput = input.sqrMagnitude > inputDeadzone * inputDeadzone;

        if (!hasInput)
        {
            _velocity.x = 0f;
            _velocity.z = 0f;
            return;
        }

        // 1. Build the desired direction relative to the camera, flattened to the ground plane
        Transform cam = _playerLook.CameraHolder;

        Vector3 camForward = cam.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 desiredDirection = (camRight * input.x + camForward * input.y).normalized;

        // 2. Rotate the player toward that direction
        Quaternion targetRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        // 3. Always travel along the player's own forward
        float currentSpeed = _inputHandler.SprintActive ? speed * sprintMultiplier : speed;
        float inputStrength = Mathf.Clamp01(input.magnitude); // keeps gamepad analog control

        Vector3 movement = transform.forward * (currentSpeed * inputStrength);
        _velocity.x = movement.x;
        _velocity.z = movement.z;

        if (_controller.isGrounded)
        {
            OnMovement?.Invoke();
        }
    }

    private void UpdateJumpPressed()
    {
        if (_controller.isGrounded && _inputHandler.JumpPressed)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityConstant);
        }
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }
        _velocity.y += gravityConstant * Time.deltaTime;
    }
}