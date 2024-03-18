using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelecteableMenuItem : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerClickHandler, ISubmitHandler
{
    [Header("Component references")]
    [SerializeField, System.NonSerialized] RectTransform rectTransform;
    [SerializeField] BlinkingText blinkingText;

    [Header("External References")]
    [SerializeField] RectTransform indicatorRectTransform;

    [Header("Blink Settings")]
    [SerializeField] bool blinkOnSubmit;

    [Header("Submit Events")]
    [SerializeField] UnityEvent OnSubmitEvent;


    public void OnSelect(BaseEventData eventData)
    {
        float leftPosition = rectTransform.rect.width / 2f;
        indicatorRectTransform.position = rectTransform.position;
        indicatorRectTransform.anchoredPosition += new Vector2(-leftPosition, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public void OnPointerClick(PointerEventData eventData) => OnSubmit(eventData);
    public void OnSubmit(BaseEventData eventData)
    {
        IEnumerator HandleSubmit()
        {
            GameInputManager.Instance.Deactivate();

            if (blinkOnSubmit)
                yield return blinkingText.SubmitBlinking();

            GameInputManager.Instance.EnableUi();
            OnSubmitEvent?.Invoke();
        }
        StartCoroutine(HandleSubmit());

    }

    void OnValidate()
    {
        rectTransform = GetComponent<RectTransform>();
        blinkingText = GetComponentInChildren<BlinkingText>();
        indicatorRectTransform = GameObject.Find("SelectIndicator").GetComponent<RectTransform>();
    }
}
