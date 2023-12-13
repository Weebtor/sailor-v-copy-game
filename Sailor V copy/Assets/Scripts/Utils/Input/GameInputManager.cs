using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager instance;
    // Start is called before the first frame update
    public bool JumpJustPressed { get; private set; }
    public bool ShootJustPressed { get; private set; }
    public bool CrouchButtonDown { get; private set; }
    public bool CrouchButtonHold { get; private set; }
    public bool MenuOpenCloseInput { get; private set; }

    private PlayerInput _playerInput;

    private InputAction _jumpAction;
    private InputAction _crouchAction;
    private InputAction _shootAction;
    private InputAction _menuOpenCloseAction;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _playerInput = GetComponent<PlayerInput>();
        SetupInputActions();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
    }

    private void SetupInputActions()
    {
        _jumpAction = _playerInput.actions["Jump"];
        _crouchAction = _playerInput.actions["Crouch"];
        _shootAction = _playerInput.actions["Shoot"];
        _menuOpenCloseAction = _playerInput.actions["MenuOpenClose"];
    }

    private void UpdateInputs()
    {
        JumpJustPressed = _jumpAction.WasPressedThisFrame();
        ShootJustPressed = _shootAction.WasPressedThisFrame();
        CrouchButtonDown = _crouchAction.WasPressedThisFrame();
        CrouchButtonHold = _crouchAction.IsPressed();
        MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();

    }
}
