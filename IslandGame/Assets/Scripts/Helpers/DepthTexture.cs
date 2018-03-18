using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DepthTexture : MonoBehaviour
{
    private Camera cam;

    public bool invert;

    // Use this for initialization
    void Start ()
    {
        cam = GetComponent<Camera>();
        if (!invert)
        {
            cam.depthTextureMode = DepthTextureMode.Depth;
        }
        else
        {
            cam.depthTextureMode = DepthTextureMode.None;
        }
	}
}
