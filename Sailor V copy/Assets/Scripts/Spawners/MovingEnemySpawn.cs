using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemySpawn : BaseSpawner
{
    [SerializeField] float heightOffset = 10f;


    public override void SpawnEnemy()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        float randomPoint = Random.Range(lowestPoint, highestPoint);
        Vector3 startPosition = new Vector3(transform.position.x, randomPoint, 0);
        Instantiate(enemyPrefab, startPosition, transform.rotation);

    }

    void OnDrawGizmosSelected()
    {
        Vector3 center = transform.position;
        Vector3 normalUp = transform.up.normalized;
        Vector3 point1 = center + (normalUp * heightOffset);
        Vector3 point2 = center - (normalUp * heightOffset);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(point1, point2);
    }
}
