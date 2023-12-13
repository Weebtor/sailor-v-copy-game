using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] GameObject playerObject;
    Player player;
    public int Score { get; private set; }
    public int Hearts { get; private set; }

    void Start()
    {
        player = playerObject.GetComponent<Player>();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void IncrementScore() { Score++; }
    public void DecreaseHearts() { Hearts--; }
    public void RecoverHealth() { }
    public void OnPlayerDead()
    {
        Debug.Log("On player dead");
    }
}
