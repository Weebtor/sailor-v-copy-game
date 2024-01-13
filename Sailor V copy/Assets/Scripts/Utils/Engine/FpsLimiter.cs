using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public static FpsSettings instance;
    readonly int defaultFps = 60;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        SetMaxFps(defaultFps);
    }

    void SetMaxFps(int maxFps)
    {
        Application.targetFrameRate = maxFps;
    }
}
