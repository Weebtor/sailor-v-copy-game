using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


enum ImageEnum
{
    Normal,
    Dead,
    Win,
}
public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] hearts;
    [SerializeField] Sprite[] sprites;
    [SerializeField] int currentHearts, maxHearts;

    void Start()
    {
        currentHearts = maxHearts = hearts.Length;
    }


    public void PlayerTakeDamage(Component sender, object data)
    {
        if (data is not int) return;

        int value = (int)data;

        for (int i = 0; i < value; i++)
        {
            if (currentHearts <= 0) return;

            // hearts[currentHearts - 1].SetActive(false);
            hearts[currentHearts - 1].GetComponent<Image>().sprite = sprites[(int)ImageEnum.Dead];
            currentHearts -= 1;
        }
    }

    public void RecoverDamage(int recoverPoints = 1)
    {

        for (int i = 0; i < recoverPoints; i++)
        {
            if (currentHearts == maxHearts) return;

            currentHearts += 1;
            hearts[currentHearts - 1].SetActive(true);
        }
    }

}
