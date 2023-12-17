using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Scene components")]
    [SerializeField] PlayerStateManager playerStateManager;
    [Header("Stage condition")]
    [System.NonSerialized] bool stageCompleted = false;
    [Header("Score")]
    [System.NonSerialized] int score = 0;



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
