using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PressEnterScript : MonoBehaviour, ISubmitHandler, ISelectHandler
{
    [SerializeField] BlinkingText blinkingText;
    [SerializeField] UnityEvent OnSubmitEvent;

    bool isInitialized = false;
    void HandleOnEneable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    void Start()
    {
        HandleOnEneable();
        isInitialized = true;
    }
    void OnEnable()
    {
        if (isInitialized == true)
            HandleOnEneable();
    }
    public void OnSelect(BaseEventData eventData)
    {
        blinkingText.IdleBlinking();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        IEnumerator HandleSubmit()
        {
            yield return blinkingText.SubmitBlinking();
            OnSubmitEvent?.Invoke();
        }
        StartCoroutine(HandleSubmit());
    }
}
