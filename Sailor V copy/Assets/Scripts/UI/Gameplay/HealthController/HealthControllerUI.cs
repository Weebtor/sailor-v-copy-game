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
public class HealthControllerUI : MonoBehaviour
{
    [SerializeField] Transform[] livesSprite = new Transform[maxLives];

    int HealthUI;
    const int maxLives = 10;
    GameObject playerTarget;
    PlayerHealthController playerHealthController;

    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        playerHealthController = playerTarget.GetComponent<PlayerHealthController>();
        SetupLives();
    }

    void SetupLives()
    {
        HealthUI = playerHealthController.GetMaxHealth();
        for (int i = 0; i < 10; i++)
        {
            livesSprite[i] = transform.GetChild(i);
            if (i < HealthUI) livesSprite[i].gameObject.SetActive(true);
        }
    }


    void HandlePlayerHealthChanged()
    {
        int playerHealth = playerHealthController.GetCurrentHealth();

        if (HealthUI == playerHealth) return;
        if (playerHealth > HealthUI)
            IncreaseHealth(playerHealth - HealthUI);
        else
            DecreaseHealth(HealthUI - playerHealth);

    }

    void IncreaseHealth(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            livesSprite[HealthUI].gameObject.SetActive(true);
            HealthUI += 1;
        }
    }
    void DecreaseHealth(int amount)
    {
        int playerHealth = playerHealthController.GetCurrentHealth();
        while (HealthUI > playerHealth)
        {
            livesSprite[HealthUI - 1].gameObject.SetActive(false);
            HealthUI += -1;
        }

    }
    // EVENT LISTENER
    public void OnListenerPlayerTakeDamage(Component sender, object data)
    {
        // if (data is not int) return;
    }

    public void OnListenerPlayerHealthChanged(Component sender, object data)
    {
        HandlePlayerHealthChanged();
    }

}
