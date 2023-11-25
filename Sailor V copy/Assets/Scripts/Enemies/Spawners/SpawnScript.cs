using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : BaseSpawner
{
    // Start is called before the first frame update
    [SerializeField] float heightOffset = 10f;
    [SerializeField] float sideOffset = 1f;


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

        // cambiar a utilsGizmoBox
        Vector3 center = transform.position;
        Vector3 rotationVector = transform.right.normalized;
        Quaternion rotation = Quaternion.LookRotation(rotationVector);



        Vector3 topLeft = center + rotation * new Vector3(-sideOffset, heightOffset);
        Vector3 topRight = center + rotation * new Vector3(sideOffset, heightOffset);
        Vector3 bottomLeft = center + rotation * new Vector3(-sideOffset, -heightOffset);
        Vector3 bottomRight = center + rotation * new Vector3(sideOffset, -heightOffset);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
