using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform idleTransform;
    [SerializeField] Transform crouchingTransform;
    [SerializeField] Transform jumpingTransform;


    [SerializeField] GameObject bulletPrefab;
    [SerializeField] CoolDown weaponCooldown;

    MovementStateManager playerState;
    Animator playerAnimator;
    PlayerAnimationHandler playerAnimationHandler;

    void Start()
    {
        playerState = transform.root.GetComponentInChildren<MovementStateManager>();
        playerAnimationHandler = transform.root.GetComponentInChildren<PlayerAnimationHandler>();
        playerAnimator = transform.root.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleShoot();
    }


    void HandleShoot()
    {

        if (UserInputScript.instance.ShootJustPressed && weaponCooldown.IsCoolingDown == false)
        {
            weaponCooldown.StartCooldown();
            playerAnimationHandler.HandleShootAnimation();
            SpawnBullet();
        }
    }


    void SpawnBullet()
    {
        var currentState = playerState.GetCurrentState();
        Vector2 spawnPosition = currentState switch
        {
            PlayerIdleState => idleTransform.position,
            PlayerCrouchingState => crouchingTransform.position,
            PlayerJumpingState => jumpingTransform.position,
            PlayerFallingState => jumpingTransform.position,
            _ => Vector2.zero,
        };
        // change for pooling in the future
        Instantiate(bulletPrefab, spawnPosition, transform.rotation);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; Gizmos.DrawSphere(idleTransform.position, 0.02f);
        Gizmos.color = Color.blue; Gizmos.DrawSphere(crouchingTransform.position, 0.02f);
        Gizmos.color = Color.yellow; Gizmos.DrawSphere(jumpingTransform.position, 0.02f);
    }
}
