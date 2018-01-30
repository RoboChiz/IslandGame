using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsPrefab : MonoBehaviour
{
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    public void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        if (FindObjectOfType<BuildingModeManager>() != null && FindObjectOfType<BuildingModeManager>().isActivated)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void Reset()
    {
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
    }
}
