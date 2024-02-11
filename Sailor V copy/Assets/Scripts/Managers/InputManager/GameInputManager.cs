using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager Instance;
    public PlayerInput PlayerInputs { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        PlayerInputs = GetComponent<PlayerInput>();
    }


    public void EnableUi()
    {
        PlayerInputs.SwitchCurrentActionMap("UI");
    }

    public void EnablePlayer()
    {
        PlayerInputs.SwitchCurrentActionMap("Player");
    }
}
