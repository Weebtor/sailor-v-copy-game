using UnityEngine;
using UnityEngine.UI;

public static class ObjectUtilities
{
    public static void FlipAxisX(Transform gameObject)
    {
        gameObject.transform.Rotate(0, 180, 0);
    }
}


