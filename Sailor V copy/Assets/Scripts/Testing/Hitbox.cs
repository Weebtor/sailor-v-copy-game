using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    Collider2D box;
    private void Awake()
    {
        box = GetComponent<Collider2D>();
        box.isTrigger = true;
        gameObject.layer = (int)General.Layers.Hitbox;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     int otherLayer = other.gameObject.layer;
    //     if (otherLayer == (int)General.Layers.Hurtbox)
    //     {
    //         Debug.Log($"<color=green> {this}: do damage on {other.name}/{other.transform.parent?.name}</color>");
    //     }
    //     else if (otherLayer == (int)General.Layers.Hitbox)
    //     {

    //         Debug.Log($"<color=yellow> {this}: clash on {other.name}/{other.transform.parent?.name}</color>");
    //     }
    //     Debug.Log($"<color=yellow> Trigger {this} on {other.name}</color>");
    //     Debug.Log($"<color=yellow> root: {other.transform.root.name}</color>");
    //     Debug.Log($"<color=yellow> layer: {other.gameObject.layer}</color>");
    //     Debug.Log($"<color=yellow> parent: {other.transform.parent?.name}</color>");
    // }
}
