using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : BaseSpawner
{
    // Start is called before the first frame update
    [SerializeField] float heightOffset = 10f;


    // void Start()
    // {
    //     StartCoroutine(TestGenerating());
    // }

    // Update is called once per frame

    // [ContextMenu("Start Generating")]
    // IEnumerator TestGenerating()
    // {
    //     for (int i = 0; i < 10; i++)
    //     {
    //         Debug.Log("spawn bird");
    //         SpawnBird();
    //         yield return new WaitForSeconds(2.0f);
    //     }
    // }



    public override void SpawnEnemy()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        float randomPoint = Random.Range(lowestPoint, highestPoint);
        Vector3 startPosition = new Vector3(transform.position.x, randomPoint, 0);
        Instantiate(enemyPrefab, startPosition, transform.rotation);

    }

    void OnDrawGizmos()
    {

        Vector3 center = transform.position;
        Vector3 normalUp = transform.up.normalized;

        Vector3 point1 = center + (normalUp * heightOffset);
        Vector3 point2 = center - (normalUp * heightOffset);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(point1, point2);

    }
}
