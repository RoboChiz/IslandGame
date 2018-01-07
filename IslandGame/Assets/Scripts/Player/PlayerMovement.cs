using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f, brakeTime = 0.5f, acceleration = 2f, expectedSpeed = 0f;
    public Camera playerCamera;

    private Vector3 lastDirection;

    public Vector3 pointDirection;
	
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
            Vector3 input = new Vector3(hori, 0f, -verti).normalized;

            Vector2 inputAmount = new Vector2(hori, verti);
            float moveAmount = Mathf.Clamp(inputAmount.magnitude, 0f, 1f);

            if (moveAmount == 0)
            {
                float cacceleration = -expectedSpeed / brakeTime;
                expectedSpeed += (cacceleration * Time.fixedDeltaTime);

                if (Mathf.Abs(expectedSpeed) <= 0.1f)
                    expectedSpeed = 0;

                input = lastDirection;
            }
            else
            {            
                expectedSpeed += (moveAmount * acceleration) * Time.fixedDeltaTime;
                expectedSpeed = Mathf.Clamp(expectedSpeed, 0f, walkSpeed);

                if (moveAmount > 0.5f)
                {
                    lastDirection = input;
                }
            }

            Vector3 finalInput = cameraQuat * input * expectedSpeed;

            pointDirection = (cameraQuat * input).normalized;

            Vector3 accelerationVec = (Vector3.Scale(finalInput, new Vector3(1f, 0f, 1f)) - Vector3.Scale(rigidbody.velocity, new Vector3(1f, 0f, 1f))) / Time.fixedDeltaTime;
            rigidbody.AddForce(accelerationVec, ForceMode.Acceleration);
        }
    }
}
