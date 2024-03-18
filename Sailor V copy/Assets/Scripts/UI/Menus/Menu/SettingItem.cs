using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SettingNumericValue : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    InputAction NavigateAction => GameInputManager.Instance.PlayerInputs.actions["Navigate"];
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log($"Selected {gameObject}");
        NavigateAction.performed += OnNavigatePerformed;
        NavigateAction.canceled += OnNavigateCanceled;
    }
    public void OnDeselect(BaseEventData eventData)
    {
        // throw new System.NotImplementedException();
        Debug.Log($"Deselect {gameObject}");
        NavigateAction.performed -= OnNavigatePerformed;
        NavigateAction.canceled -= OnNavigateCanceled;

        // NavigateAction.performed -= OnNavigatePerformed;
        // NavigateAction.canceled -= OnNavigatePerformed;


    }

    float time = 0f;
    private void OnNavigatePerformed(InputAction.CallbackContext context)
    {
        Vector2 navigateValue = context.ReadValue<Vector2>();
        Debug.Log("Navigate Performed: " + navigateValue + " Time" + Time.time);
        time = Time.time;

    }
    private void OnNavigateCanceled(InputAction.CallbackContext context)
    {
        Vector2 navigateValue = context.ReadValue<Vector2>();
        Debug.Log("Navigate Canceled: " + navigateValue + " Time" + Time.time + "Total: " + (Time.time - time));

    }
    // Start is called before the first frame update

}
