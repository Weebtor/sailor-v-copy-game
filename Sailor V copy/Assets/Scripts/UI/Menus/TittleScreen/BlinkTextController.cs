using System.Collections;
using TMPro;
using UnityEngine;

public class BlinkTextController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI targetText;
    [Header("Text Settings")]
    [SerializeField] Color blinkColor = new() { a = 0 };
    [SerializeField] Color textColor;

    [Header("Animations Settings")]
    [SerializeField] BlinkSettings idle;
    [SerializeField] BlinkSettings onSubmit;

    void Awake()
    {
        targetText = GetComponentInChildren<TextMeshProUGUI>();
        textColor = targetText.color;
    }


    public Coroutine StartSubmitAnimation()
    {
        IEnumerator SubmitAnimation()
        {
            for (int i = 0; i < 3; i++)
            {
                targetText.color = blinkColor;
                yield return new WaitForSeconds(onSubmit.blinkColorTime);
                targetText.color = textColor;
                yield return new WaitForSeconds(onSubmit.textTime);
            }
        }

        return StartCoroutine(SubmitAnimation()); ;
    }


}

[System.Serializable]
public class BlinkSettings
{
    [Range(0.1f, 3f)] public float textTime = 0.5f;
    [Range(0.1f, 3f)] public float blinkColorTime = 0.5f;
}