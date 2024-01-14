using System.Collections;
using UnityEngine;



public class GameController : MonoBehaviour
{
    [Header("Scene components")]
    [SerializeField] PlayerStateController playerStateManager;

    [Header("Score")]
    [System.NonSerialized] int score = 0;

    [Header("Events")]
    public GameEvent OnStartStage;
    public GameEvent OnUpdateScore;

    // [Header("Dialogue")]
    // public DialogueObject testDialogue;
    void Start()
    {
        // DialogueManager.Instance.StartDialogue(testDialogue);
        StartStage();
    }

    void StageCompleted()
    {
        GameInputManager.Instance.GameplayActionsDisable();
        StartCoroutine(TriggerStageCompletedScreen());
    }
    IEnumerator TriggerStageCompletedScreen()
    {
        Debug.Log("Check TriggerWinScreen");
        yield return new WaitForSeconds(2f);
        playerStateManager.SwitchState(playerStateManager.WinState);
        Debug.Log("<color=green>Trigger win screen</color>");
        AudioManager.Instance.PlayMusic("victory");
    }

    [ContextMenu("Start Game")]
    public void StartStage()
    {
        OnStartStage.Raise(null, null);
        AudioManager.Instance.PlayMusic("ingame");
    }
    public void IncreaseScore(int value = 1)
    {
        score += value;
        OnUpdateScore.Raise(this, score);
        // Debug.Log($"<color=yellow>Score {score}</color>");
    }

    // EVENTS LISTENERS
    public void OnListenerStageCompleted(Component sender, object data)
    {
        StageCompleted();
    }
    public void OnListenerEnemyDefeated(Component sender, object data)
    {
        var enemy = (EnemyBase)sender;
        IncreaseScore(enemy.scorePoints);
    }
}

public enum GameState
{
    pause,
    win,
    lose,
}
