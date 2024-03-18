using System.Collections;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BlinkingText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI targetText;
    [SerializeField] float blinkingIdleSpeed = 1f;
    [SerializeField] float blinkingSubmitSpeed = 1f;


    void HideText() => targetText.alpha = 0;
    void ShowText() => targetText.alpha = 1;

    Coroutine currentCoroutine;
    Coroutine HandleNewCoroutine(IEnumerator newCoroutine)
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(newCoroutine);
        return currentCoroutine;
    }

    IEnumerator BlinkingLoop()
    {
        while (true)
        {
            HideText();
            yield return new WaitForSeconds(0.5f / blinkingIdleSpeed);
            ShowText();
            yield return new WaitForSeconds(0.5f / blinkingIdleSpeed);
        }
    }
    IEnumerator BlinkingSubmit()
    {
        for (int i = 0; i < 3; i++)
        {
            HideText();
            yield return new WaitForSeconds(0.5f / blinkingSubmitSpeed);
            ShowText();
            yield return new WaitForSeconds(0.5f / blinkingSubmitSpeed);
        }
    }

    public Coroutine IdleBlinking()
    {
        return HandleNewCoroutine(BlinkingLoop());
    }

    public Coroutine SubmitBlinking()
    {
        return HandleNewCoroutine(BlinkingSubmit());
    }






}
