using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    [Header("Input system")]
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


    public void Deactivate() => PlayerInputs.DeactivateInput();
    public void Activate() => PlayerInputs.ActivateInput();
    public void EnableUi() => PlayerInputs.SwitchCurrentActionMap("UI");
    public void EnablePlayer() => PlayerInputs.SwitchCurrentActionMap("Player");


}
