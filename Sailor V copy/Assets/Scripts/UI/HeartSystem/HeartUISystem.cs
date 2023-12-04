using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUISystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] hearts;
    [SerializeField] int currentHearts, maxHearts;

    void Start()
    {
        currentHearts = maxHearts = hearts.Length;
    }


    void TakeDamage(int damage = 1)
    {
        for (int i = 0; i < damage; i++)
        {
            if (currentHearts <= 0) return;

            hearts[currentHearts - 1].SetActive(false);
            currentHearts -= 1;
        }
    }

    void RecoverDamage(int recoverPoints = 1)
    {

        for (int i = 0; i < recoverPoints; i++)
        {
            if (currentHearts == maxHearts) return;

            currentHearts += 1;
            hearts[currentHearts - 1].SetActive(true);
        }
    }
    [ContextMenu("Test damage")]
    void TestDamage() { TakeDamage(1); }

    [ContextMenu("Recover damage")]
    void TestRecover() { RecoverDamage(1); }
}
