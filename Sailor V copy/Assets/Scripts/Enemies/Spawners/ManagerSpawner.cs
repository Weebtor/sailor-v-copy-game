using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGeneratorManager : MonoBehaviour
{
    [SerializeField] BaseSpawner[] spawnersArray;

    void Start()
    {
        spawnersArray = GetComponentsInChildren<BaseSpawner>();

        Debug.Log(spawnersArray.Length);
        foreach (var child in spawnersArray)
        {
            child.SayHello();
        }

    }
    [ContextMenu("Generate random enemy")]
    void GenerateRandomEnemy()
    {
        int indexOption = Random.Range(0, spawnersArray.Length);
        var spawnerScript = spawnersArray[indexOption];
        spawnerScript.SpawnEnemy();
    }
}
