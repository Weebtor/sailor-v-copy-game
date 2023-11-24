using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector2 groundPoint;
    [SerializeField] Vector2 crouchingPoint;
    [SerializeField] Vector2 jumpingPoint;
    Vector2 currentPoint;

    [SerializeField] GameObject bulletPrefab;
    GameObject currentBullet;


    MovementStateManager movementStateManager;

    void Start()
    {
        currentPoint = crouchingPoint;
        GameObject parent = transform.parent.gameObject;
        movementStateManager = parent.GetComponentInChildren<MovementStateManager>();

    }

    // Update is called once per frame
    void Update()
    {
        HandleShoot();
    }


    void HandleShoot()
    {
        if (UserInputScript.instance.ShootJustPressed && currentBullet == null)
        {
            SpawnBullet();
        }
    }


    void SpawnBullet()
    {
        var currentState = movementStateManager.GetCurrentState();
        Vector2 offset = currentState switch
        {
            PlayerGroundedState => groundPoint,
            PlayerCrouchingState => crouchingPoint,
            PlayerJumpingState => jumpingPoint,
            _ => Vector2.zero,
        };

        currentBullet = Instantiate(bulletPrefab, offset + (Vector2)transform.position, transform.rotation);
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; Gizmos.DrawSphere(transform.position + (Vector3)groundPoint, 0.02f);
        Gizmos.color = Color.blue; Gizmos.DrawSphere(transform.position + (Vector3)crouchingPoint, 0.02f);
        Gizmos.color = Color.yellow; Gizmos.DrawSphere(transform.position + (Vector3)jumpingPoint, 0.02f);
    }
}
