using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour
{
    [field: SerializeField] protected GameObject enemyPrefab;
    public abstract void SpawnEnemy();
}
