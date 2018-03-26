using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    private GameObject footstepPrefab;
    private AudioClip footstepNoise;

    public Transform[] feet;
    public AudioSource footStepNoiseMaker;

    public float checkDistance = 0.2f, spacing = 0.2f;
    private Vector3[] lastSpots;

    List<FootStep> steps;

	// Use this for initialization
	void Start ()
    {
        footstepPrefab = Resources.Load<GameObject>("Prefabs/Footprint");
        footstepNoise = Resources.Load<AudioClip>("SFX/footstep");

        steps = new List<FootStep>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(lastSpots == null || lastSpots.Length != feet.Length)
        {
            lastSpots = new Vector3[feet.Length];
        }

        int count = 0;
		foreach(Transform foot in feet)
        {
            RaycastHit hit;
            if(Physics.Raycast(foot.position, -foot.up, out hit, checkDistance ) && hit.transform.tag == "Sand")
            {
                Vector3 spot = hit.point + (hit.normal.normalized * 0.05f);
                if (Vector3.Distance(spot, lastSpots[count]) > spacing)
                {
                    lastSpots[count] = hit.point;

                    GameObject step = Instantiate(footstepPrefab, spot, Quaternion.LookRotation(-hit.normal, transform.forward));
                    step.GetComponent<MeshRenderer>().material = new Material(step.GetComponent<MeshRenderer>().material);
                    step.GetComponent<MeshRenderer>().material.color = Color.clear;

                    steps.Add(new FootStep(step));

                    if(footStepNoiseMaker != null)
                    {
                        footStepNoiseMaker.PlayOneShot(footstepNoise);
                    }
                }
            }

            count++;
        }

        float diff = FootStep.aliveTime - FootStep.fadeTime;
        foreach (FootStep step in steps.ToArray())
        {
            if (step.timer > 0f)
            {
                step.timer -= Time.deltaTime;

                if (step.timer > diff)
                {
                    //Fade In
                    step.prefabClone.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.clear, Color.white, (FootStep.fadeTime - (step.timer - diff)) / FootStep.fadeTime);
                }
                else if (step.timer < FootStep.fadeTime)
                {
                    step.prefabClone.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.clear, (FootStep.fadeTime - step.timer) / FootStep.fadeTime);
                }
                else
                {
                    step.prefabClone.GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
            else
            {
                Destroy(step.prefabClone);
                steps.Remove(step);
            }
        }
    }

    class FootStep
    {
        public GameObject prefabClone;
        public float timer;
        public const float aliveTime = 5f, fadeTime = 0.5f;

        public FootStep(GameObject _prefab)
        {
            prefabClone = _prefab;
            timer = aliveTime;
        }
    }
}
