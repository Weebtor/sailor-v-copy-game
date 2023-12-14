using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnController : MonoBehaviour
{

    [Header("Required components")]
    [SerializeField] BaseSpawner[] spawnersArray;

    [Header("Wave controller")]
    [SerializeField] int enemiesPerWave = 5;
    [System.NonSerialized] int enemyLeft;
    [SerializeField] float spawnColldown = 5f;


    void Start()
    {
        if (spawnersArray.Length == 0)
            spawnersArray = GetComponentsInChildren<BaseSpawner>();
    }


    [ContextMenu("Start Wave")]
    public void StartWave()
    {
        enemyLeft = enemiesPerWave;
        StartCoroutine(TestGenerating());
    }

    [ContextMenu("Generate random enemy")]
    void GenerateRandomEnemy()
    {
        int indexOption = Random.Range(0, spawnersArray.Length);
        BaseSpawner spawnerScript = spawnersArray[indexOption];
        spawnerScript.SpawnEnemy();
    }



    IEnumerator TestGenerating()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            GenerateRandomEnemy();
            yield return new WaitForSeconds(spawnColldown);
        }
    }



}
