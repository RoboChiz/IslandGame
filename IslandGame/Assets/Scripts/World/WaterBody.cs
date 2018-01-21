using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class WaterBody : MonoBehaviour
{
    public float buoyancy = 2f, offsetHeight = 0.1f;

    private void Awake()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    private void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        //Do Water Splash

       
    }

    public void OnTriggerExit(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.inWater = false;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody)
        {
            //Set In Water for Player
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.inWater = true;
            }

            //If something is registered as In Water, do Raycasts to determine depth .etc
            Vector3 objectPos = other.transform.position;

            RaycastHit hit;
            Physics.Raycast(objectPos + (Vector3.up * 100f), Vector3.down, out hit, 200f, LayerMask.GetMask("Water"), QueryTriggerInteraction.Collide);

            if(hit.transform == null)
            {
                Debug.Log("Something's Wrong!");
            }
            else
            {
                //Apply Water Physics to Object
                float depth = objectPos.y - hit.point.y; //Should only be in the y direction

                depth += Mathf.Sin(Time.time) * offsetHeight;

                Vector3 newVelocity = -Vector3.up * depth * buoyancy;

                Vector3 accelerationVec = (Vector3.Scale(newVelocity, new Vector3(0f, 1f, 1f)) - Vector3.Scale(other.attachedRigidbody.velocity, new Vector3(0f, 1f, 0f))) / Time.fixedDeltaTime;
                other.attachedRigidbody.AddForce(accelerationVec, ForceMode.Acceleration);
            }          
        }
    }
}
