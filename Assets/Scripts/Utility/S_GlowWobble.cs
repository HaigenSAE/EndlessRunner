using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GlowWobble : MonoBehaviour
{

    public float ampMax, ampMin;
    private Vector2 scale;

    // Update is called once per frame
    void Update()
    {
        //simple glow wobble
        for (int i = 0; i < 2; ++i)
        {
            scale[i] = ampMax + ampMin * Mathf.Sin(Time.time * 2);
        }
        transform.localScale = scale;
    }
}
