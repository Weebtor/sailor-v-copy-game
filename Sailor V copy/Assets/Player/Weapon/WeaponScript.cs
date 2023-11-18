using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bulletPrefab;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        Vector3 spawnPoint = transform.position;
        Instantiate(bulletPrefab, spawnPoint, transform.rotation);
    }
}
