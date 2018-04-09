using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public GameObject localObject;
    public MeshRenderer actualObject;
    public ParticleSystem puffParticles;

    private const float slamSpeed = 0.075f;
    public AnimationCurve slamRate;

    public AudioClip slamNoise;

    // Use this for initialization
    IEnumerator Start ()
    {
        StartCoroutine(SlamItem());
        yield return new WaitForSeconds(slamSpeed * 0.5f);
        puffParticles.Play();
        FindObjectOfType<SoundManager>().PlaySFX(slamNoise, 0.2f);
    }
	
	private IEnumerator SlamItem()
    {
        Transform itemTransform = localObject.transform;
        Vector3 endPosition = itemTransform.position, startPosition = itemTransform.position + new Vector3(0f, 0.5f, 0f);

        if (actualObject != null)
        {
            actualObject.enabled = false;
        }

        float startTime = Time.time;
        while(Time.time - startTime < slamSpeed)
        {
            itemTransform.position = Vector3.Lerp(startPosition, endPosition, slamRate.Evaluate((Time.time - startTime) / slamSpeed));
           yield return null;
        }

        itemTransform.position = endPosition;
        if (actualObject != null)
        {
            actualObject.enabled = true;
            localObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
