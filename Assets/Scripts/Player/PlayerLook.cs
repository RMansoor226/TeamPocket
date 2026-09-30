using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraHolder;

    [Header("Follow")]
    [SerializeField] private Vector3 followOffset = new Vector3(0f, 1.5f, 0f); // pivot near head/shoulders

    [Header("Look")]
    [SerializeField] private float lookSensitivity = 0.1f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;

    private PlayerInputHandler _inputHandler;
    private float _pitch;
    private float _yaw;

    // Exposed so PlayerMove can move relative to the camera
    public Transform CameraHolder => cameraHolder;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();

        if (_inputHandler == null)
            Debug.LogError("PlayerLook: PlayerInputHandler missing on this object.", this);

        if (cameraHolder == null)
            Debug.LogError("PlayerLook: CameraHolder not assigned in the Inspector.", this);
    }

    private void Start()
    {
        // Start the camera facing the same way as the player
        _yaw = transform.eulerAngles.y;
    }

    private void LateUpdate()
    {
        UpdateLook();
        FollowPlayer();
    }

    private void UpdateLook()
    {
        Vector2 look = _inputHandler.LookInput;

        // Mouse delta is already per-frame, so no Time.deltaTime here
        _yaw += look.x * lookSensitivity;
        _pitch -= look.y * lookSensitivity;
        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);

        cameraHolder.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }

    private void FollowPlayer()
    {
        cameraHolder.position = transform.position + followOffset;
    }

    public void SetSensitivity(float sensitivity)
    {
        lookSensitivity = sensitivity;
    }
}