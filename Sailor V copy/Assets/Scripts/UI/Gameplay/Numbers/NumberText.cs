using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberText : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text scoreText;

    public void UpdateScoreText(int value)
    {
        scoreText.text = value.ToString();
    }

    public void OnListenerUpdateNumber(Component sender, object data)
    {
        if (data is not int)
        {
            Debug.LogError("invalid data type MUST be INTEGER");
            return;
        }

        UpdateScoreText((int)data);
    }

}
