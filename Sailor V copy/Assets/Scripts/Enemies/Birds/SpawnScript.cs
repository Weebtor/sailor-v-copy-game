using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject birdPrefab;
    [SerializeField] float heightOffset = 10f;
    [SerializeField] float sideOffset = 1f;


    void Start()
    {
        StartCoroutine(TestGenerating());
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Start Generating")]
    IEnumerator TestGenerating()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("spawn bird");
            SpawnBird();
            yield return new WaitForSeconds(1.0f);
        }
    }



    void SpawnBird()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        float randomPoint = Random.Range(lowestPoint, highestPoint);
        Vector3 startPosition = new Vector3(transform.position.x, randomPoint, 0);
        Instantiate(birdPrefab, startPosition, transform.rotation);

    }

    void OnDrawGizmos()
    {

        // cambiar a utilsGizmoBox
        Vector3 center = transform.position;
        Vector3 topLeft = center + new Vector3(sideOffset, -heightOffset);
        Vector3 topRight = center + new Vector3(sideOffset, heightOffset);
        Vector3 bottomLeft = center + new Vector3(-sideOffset, -heightOffset);
        Vector3 bottomRight = center + new Vector3(-sideOffset, heightOffset);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
