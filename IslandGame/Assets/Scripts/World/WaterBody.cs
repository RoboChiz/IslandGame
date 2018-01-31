using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class WaterBody : MonoBehaviour
{
    public float buoyancy = 10f, offsetHeight = 0.05f;

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
            bool ignorePhysics = false;
            Vector3 objectPos = other.transform.position;

            //If something is registered as In Water, do Raycasts to determine depth .etc         
            RaycastHit hit;
            Physics.Raycast(objectPos + (Vector3.up * 100f), Vector3.down, out hit, 200f, LayerMask.GetMask("Water"), QueryTriggerInteraction.Collide);

            //Set In Water for Player
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                bool isActuallyInWater = true;

                //Do Ground Check
                RaycastHit groundHit = new RaycastHit();
                if (Physics.Raycast(objectPos, Vector3.down, out groundHit, 15f, ~(LayerMask.GetMask("Water") | LayerMask.GetMask("Ignore Raycast")), QueryTriggerInteraction.Collide))
                {
                    float groundWaterDiff = hit.point.y - groundHit.point.y;

                    //If the difference between the top of the water and the ground is less than the height of the player, presume we're above ground
                    if(groundWaterDiff < 0.5f)
                    {
                        isActuallyInWater = false;
                    }
                }

                player.inWater = isActuallyInWater;

                if(player.isJumping || !player.inWater)
                {
                    ignorePhysics = true;
                }
            }

            if (!ignorePhysics)
            {
                
                if (hit.transform == null)
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
}
