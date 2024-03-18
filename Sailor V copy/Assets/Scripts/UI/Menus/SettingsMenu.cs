using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject startSelected;

    bool isInitalized = false;
    void HandleOnEnable()
    {
        EventSystem.current.SetSelectedGameObject(startSelected);
    }
    void Start()
    {
        HandleOnEnable();
        isInitalized = true;

    }
    void OnEnable()
    {
        if (isInitalized)
            HandleOnEnable();
    }

    public void Menu_SetMasterVolume(float value)
    {
        Debug.Log($"[New Value]:{value} ");

    }
    public void Menu_SetMusicVolume(float value) { }
    public void Menu_SetSfxVolume(float value) { }
}
