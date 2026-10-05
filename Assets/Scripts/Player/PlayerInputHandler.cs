using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls _playerControls;
    
    // Pause Manager will be implemented in the future
    // [SerializeField]
    // private PauseManager pauseManager;
    
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    
    public bool JumpPressed { get; private set; }
    public bool SprintActive { get; private set; }
    
    public bool IsAttacking { get; private set; }
    public bool IsCastingSpell { get; private set; }
    public bool PausedGame { get; private set; }
    
    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void OnEnable()
    {
        _playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Player.Disable();
    }
    
    public void DisableSelf() // made for player health's OnDeath event to disable input upon death
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        MoveInput = Vector2.zero;
        LookInput = Vector2.zero;
        JumpPressed = false;
        // idk if anything else is needed, but i figure this is good

        this.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        PausedGame = _playerControls.Player.Pause.WasPressedThisFrame();
        // if (PausedGame)
        // {
        //     PauseGame();
        // }


        MoveInput = _playerControls.Player.Move.ReadValue<Vector2>();
        LookInput = _playerControls.Player.Look.ReadValue<Vector2>();
    
        JumpPressed = _playerControls.Player.Jump.WasPressedThisFrame();
        SprintActive = _playerControls.Player.Sprint.IsPressed();
        IsAttacking = _playerControls.Player.Attack.WasPressedThisFrame();
        IsCastingSpell = _playerControls.Player.CastSpell.WasPressedThisFrame();
    }

    // public void PauseGame()
    // {
    //     if (!pauseManager.IsPaused)
    //     {
    //         pauseManager.Pause();
    //     }
    //     else
    //     {
    //         pauseManager.Resume();
    //     }
    // }
}

