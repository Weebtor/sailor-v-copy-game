using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesWaveController : MonoBehaviour
{

    [Header("Required components")]
    [SerializeField] BaseSpawner[] spawnersArray;

    [Header("Wave controller")]
    [SerializeField] float spawnColldown = 5f;
    [SerializeField] int enemiesPerWave = 5;
    [SerializeField] int enemiesCounter;

    [Header("Events")]
    public GameEvent stageCompleted;
    public GameEvent updateEnemyCounter;
    void Start()
    {
        if (spawnersArray.Length == 0)
            spawnersArray = GetComponentsInChildren<BaseSpawner>();
    }

    public void StartWave(Component sender, object data)
    {
        enemiesCounter = enemiesPerWave;
        StartCoroutine(GenerateWave());
    }

    void GenerateRandomEnemy()
    {
        int indexOption = Random.Range(0, spawnersArray.Length);
        BaseSpawner spawnerScript = spawnersArray[indexOption];
        spawnerScript.SpawnEnemy();
    }
    IEnumerator GenerateWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            GenerateRandomEnemy();
            yield return new WaitForSeconds(spawnColldown);
        }
    }



    public void DecreaseRemainingEnemiesCounter()
    {
        enemiesCounter -= 1;
        updateEnemyCounter.Raise(this, enemiesCounter);
        if (enemiesCounter == 0)
        {
            stageCompleted.Raise(this);
        }
    }


    // EVENT LISTENERS
    public void OnListenerEnemyDestroyed(Component sender, object data)
    {
        DecreaseRemainingEnemiesCounter();
    }


}
