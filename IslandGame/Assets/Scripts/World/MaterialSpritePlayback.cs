using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialSpritePlayback : MonoBehaviour
{
    public Texture2D[] sprites;
    public float timePerFrame = 0.1f;

    private float timeSinceLastFrame;

    public Material material;
    private int currentFrame = 0;

    public string textureName = "Caustic Map";

    // Update is called once per frame
    void Update ()
    {
        if (material != null)
        {
            if (timeSinceLastFrame < 0f)
            {
                timeSinceLastFrame = timePerFrame;
                currentFrame = MathHelper.NumClamp(currentFrame + 1, 0, sprites.Length);
                material.SetTexture(textureName, sprites[currentFrame]);
            }
            else
            {
                timeSinceLastFrame -= Time.deltaTime;
            }
        }
	}
}
