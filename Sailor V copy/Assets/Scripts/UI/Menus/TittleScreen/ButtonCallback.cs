using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NavigationButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("On Trigger Animation")]
    [SerializeField] TextMeshProUGUI targetText;
    [SerializeField] Color textColorNormal = Color.white;
    [SerializeField] Color textColorBlink = new() { a = 0 };
    [SerializeField] float fadeOutBlinkTime = 0.1f;
    // NavigationMenu navigationMenu;

    public UnityEvent callback;
    public delegate void ButtonCallbackFunction();
    public ButtonCallbackFunction callbackFunction;

    void Awake()
    {
        // navigationMenu = transform.root.GetComponentInChildren<NavigationMenu>();
        targetText = GetComponent<TextMeshProUGUI>();
    }
    void NormalText() => targetText.color = textColorNormal;
    void BlinkText() => targetText.color = textColorBlink;
    public void TriggerCallback()
    {
        StartCoroutine(OnButtonPress());
    }

    IEnumerator OnButtonPress()
    {
        NormalText(); yield return new WaitForSeconds(fadeOutBlinkTime);
        BlinkText(); yield return new WaitForSeconds(fadeOutBlinkTime);
        NormalText(); yield return new WaitForSeconds(fadeOutBlinkTime);
        BlinkText(); yield return new WaitForSeconds(fadeOutBlinkTime);
        NormalText(); yield return new WaitForSeconds(fadeOutBlinkTime);
        BlinkText(); yield return new WaitForSeconds(fadeOutBlinkTime);
        NormalText(); yield return new WaitForSeconds(fadeOutBlinkTime);
        callback.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // navigationMenu.SetSelectedButton(this);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(OnButtonPress());
    }
}

