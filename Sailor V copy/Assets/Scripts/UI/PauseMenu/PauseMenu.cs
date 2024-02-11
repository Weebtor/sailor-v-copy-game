using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    InputAction PauseAction => GameInputManager.Instance.PlayerInputs.actions["MenuOpenClose"];


    void Update()
    {
        if (PauseAction.WasPressedThisFrame() == true)
        {
            if (GameIsPaused)
                OnResume();
            else
                OnPause();
        }

    }

    void OnResume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameInputManager.Instance.EnablePlayer();

    }

    void OnPause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        GameInputManager.Instance.EnableUi();
    }

}
