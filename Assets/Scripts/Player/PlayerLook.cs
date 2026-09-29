using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private PlayerInputHandler _inputHandler;
    private Transform _cameraHolder;
    
    // Settings Manager will be implemented in the future
    // [SerializeField] 
    // private SettingsManager settingsManager;
    
    private float _verticalRotation = 0f;
    private float _horizontalRotation = 0f;
    private float _lookSensitivity = 100f;
    

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _cameraHolder = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLook();
    }
    
    private void UpdateLook()
    {
        Vector2 look = _inputHandler.LookInput;
        
        float mouseX = look.x * _lookSensitivity * Time.deltaTime;
        float mouseY = look.y * _lookSensitivity * Time.deltaTime;
        
        
        // Vertical camera rotation
        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);
        
        // Horizontal player rotation
        _horizontalRotation += mouseX;
        
        _cameraHolder.rotation = Quaternion.Euler(
            _verticalRotation, 
            _horizontalRotation, 
            0f
        );
    }

    // Sensitivity settings will be applied in the future
    
    // public void SetSensitivity(float sensitivity)
    // {
    //     _lookSensitivity = sensitivity;
    // }
}