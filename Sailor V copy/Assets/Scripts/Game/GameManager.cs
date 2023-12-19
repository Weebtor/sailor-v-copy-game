using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


public class GameManager : MonoBehaviour
{
    [Header("Scene components")]
    [SerializeField] PlayerStateManager playerStateManager;

    [Header("Stage condition")]
    [System.NonSerialized] bool stageCompleted = false;

    [Header("Score")]
    [System.NonSerialized] int score = 0;

    [Header("Events")]
    public GameState state = GameState.pause;




    // public void ResetGame()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }



    void StageCompleted()
    {
        stageCompleted = true;
        GameInputManager.instance.DisableActions();
        StartCoroutine(TriggerStageCompletedScreen());
    }
    IEnumerator TriggerStageCompletedScreen()
    {
        Debug.Log("Check TriggerWinScreen");
        yield return new WaitForSeconds(2f);
        playerStateManager.SwitchState(playerStateManager.WinState);
        Debug.Log("<color=green>Trigger win screen</color>");
        AudioManager.Instance.PlayMusic("winScreen");
    }

    void SwitchState(GameState newState)
    {
        gameState = newState;
    }
    void StartStage()
    {
        // yield return new WaitForSeconds(2f);
        // playerStateManager.SwitchState(playerStateManager.WinState);
        AudioManager.Instance.PlayMusic("inGame");
    }


    public void IncreaseScore(int value = 1)
    {
        score += value;
        Debug.Log($"<color=yellow>Score {score}</color>");

    }

    // EVENTS
    public void OnListenerStageCompleted(Component sender, object data)
    {
        StageCompleted();
    }
    public void OnListenerEnemyDestroyed(Component sender, object data)
    {
        var enemy = (EnemyBase)sender;
        IncreaseScore(enemy.scorePoints);
    }

}


enum GameState
{
    pause
}