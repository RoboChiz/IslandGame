using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSpritePlayback : MonoBehaviour
{
    private Texture2D[] sprites;
    public float timePerFrame = 0.1f;

    private float timeSinceLastFrame;

    public Material material;
    private int currentFrame = 0;

    public string textureLocation = "Caustics/";
    public string shaderTextureName = "_CausticMap";

    private bool ping = true;

    private void Awake()
    {
        sprites = Resources.LoadAll<Texture2D>(textureLocation);
    }

    // Update is called once per frame
    void Update ()
    {
        if (material != null)
        {
            if (timeSinceLastFrame < 0f)
            {
                timeSinceLastFrame = timePerFrame;

                currentFrame = MathHelper.NumClamp(currentFrame + 1, 0, sprites.Length);
                material.SetTexture(shaderTextureName, sprites[currentFrame]);
            }
            else
            {
                timeSinceLastFrame -= Time.deltaTime;
            }
        }
	}
}
