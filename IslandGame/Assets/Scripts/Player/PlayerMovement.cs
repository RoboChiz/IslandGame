using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public Camera playerCamera;
	
	// Update is called once per frame
	void Update ()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (InputManager.controllers.Count > 0)
        {
            InputDevice inputDevice = InputManager.controllers[0];

            //Get Inputs
            float hori = inputDevice.GetInput("MovementHorizontal");
            float verti = inputDevice.GetInput("MovementVertical");

            //Get Correct Camera Transform
            Vector3 cameraFoward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
            Quaternion cameraQuat = Quaternion.LookRotation(cameraFoward, Vector3.up);

            //Move the character in the direction of the camera
            Vector3 input = cameraQuat * new Vector3(hori, 0f, -verti) * walkSpeed;

            Vector3 acceleration = (Vector3.Scale(input, new Vector3(1f, 0f, 1f)) - Vector3.Scale(rigidbody.velocity, new Vector3(1f, 0f, 1f))) / Time.fixedDeltaTime;
            rigidbody.AddForce(acceleration, ForceMode.Acceleration);
        }
    }
}
