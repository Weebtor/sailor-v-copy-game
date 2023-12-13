using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject enemyPrefab;
    virtual public void SpawnEnemy() { }
    public void SayHello()
    {
        Debug.Log($"Hello {gameObject.name}");
    }
}
