using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    Collider2D box;
    private void Awake()
    {
        box = GetComponent<Collider2D>();
        box.isTrigger = false;
        gameObject.layer = (int)General.Layers.Hurtbox;
    }
}
