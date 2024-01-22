using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager Instance;
    // Start is called before the first frame update
    private PlayerInput _playerInput;

    public InputAction jumpAction { get; private set; }
    public InputAction crouchAction { get; private set; }
    public InputAction shootAction { get; private set; }
    public InputAction menuAction { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _playerInput = GetComponent<PlayerInput>();
        SetupInputActions();
        // _playerInput.SwitchCurrentActionMap()
    }


    // Update is called once per frame
    void Update()
    {
        // UpdateInputs();
    }

    private void SetupInputActions()
    {
        jumpAction = _playerInput.actions["Jump"];
        crouchAction = _playerInput.actions["Crouch"];
        shootAction = _playerInput.actions["Shoot"];
        menuAction = _playerInput.actions["MenuOpenClose"];
    }

    // private void UpdateInputs()
    // {
    //     JumpJustPressed = jumpAction.WasPressedThisFrame();
    //     ShootJustPressed = shootAction.WasPressedThisFrame();
    //     CrouchButtonDown = crouchAction.WasPressedThisFrame();
    //     CrouchButtonHold = crouchAction.IsPressed();

    //     MenuOpenCloseDown = menuAction.WasPressedThisFrame();
    //     // Debug.Log($"JumpJustPressed: {_jumpAction.WasPressedThisFrame()}");
    //     // Debug.Log($"ShootJustPressed: {_shootAction.WasPressedThisFrame()}");
    //     Debug.Log($"CrouchButtonDown: {crouchAction.WasPressedThisFrame()}");
    //     Debug.Log($"CrouchButtonHold: {crouchAction.IsPressed()}");
    // }

    public void GameplayActionsDisable()
    {
        jumpAction.Disable();
        crouchAction.Disable();
        shootAction.Disable();
    }

    public void GameplayActionsEnable()
    {
        jumpAction.Enable();
        crouchAction.Enable();
        shootAction.Enable();
    }
}
