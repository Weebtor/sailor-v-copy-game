using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGeneratorManager : MonoBehaviour
{
    [SerializeField] BaseSpawner[] spawnersArray;

    void Start()
    {
        spawnersArray = GetComponentsInChildren<BaseSpawner>();

    }
    [ContextMenu("Generate random enemy")]
    void GenerateRandomEnemy()
    {
        int indexOption = Random.Range(0, spawnersArray.Length);
        var spawnerScript = spawnersArray[indexOption];
        spawnerScript.SpawnEnemy();
    }
    // IEnumerator TestGenerating()
    // {
    //     for (int i = 0; i < 10; i++)
    //     {
    //         Debug.Log("spawn bird");
    //         SpawnBird();
    //         yield return new WaitForSeconds(2.0f);
    //     }
    // }
}
