using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidCursor : MonoBehaviour {

    public float moveSpeed = 4f, acceleration = 5f, brakeTime = 0.1f, lerpAmount = 5f;
    private float expectedSpeed = 0f, actualHeight;
    public bool isLocked = false;

    public Vector3 lastDirection { get; private set; }

    private PlayerMovement playerMovement;
    private WorldStateManager worldStateManager;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        worldStateManager = FindObjectOfType<WorldStateManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        //Get Correct Camera Transform
        Vector3 cameraFoward = Vector3.Scale(playerMovement.playerCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
        Quaternion cameraQuat = Quaternion.LookRotation(cameraFoward, Vector3.up);

        if (InputManager.controllers.Count > 0)
        {
            InputDevice inputDevice = InputManager.controllers[0];

            //Get Inputs
            float hori = inputDevice.GetInput("MovementHorizontal");
            float verti = inputDevice.GetInput("MovementVertical");
            float height = inputDevice.GetIntInputWithDelay("MovementHeight", 0.3f, Time.fixedDeltaTime);

            Vector3 input = Vector3.zero;

            if (!isLocked)
            {
                input = new Vector3(hori, 0f, -verti).normalized;
            }

            Vector2 inputAmount = new Vector2(hori, verti);
            float moveAmount = Mathf.Clamp(inputAmount.magnitude, 0f, 1f);
            float maxInput = Mathf.Max(Mathf.Abs(hori), Mathf.Abs(verti));

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
                expectedSpeed = Mathf.Clamp(expectedSpeed, 0f, moveAmount * moveSpeed);

                if (maxInput > 0.01f)
                {
                    lastDirection = input;
                }
            }

            Vector3 finalInput = cameraQuat * input * expectedSpeed;
            Vector3 accelerationVec = (Vector3.Scale(finalInput, new Vector3(1f, 0f, 1f)) - Vector3.Scale(rigidbody.velocity, new Vector3(1f, 0f, 1f))) / Time.fixedDeltaTime;
            rigidbody.AddForce(accelerationVec, ForceMode.Acceleration);

            if(height != 0)
            {
                DoHeightInput(height);
            }

            //Do Height Sliding
            transform.position = Vector3.Lerp(transform.position, GetActualPos(), Time.deltaTime * lerpAmount);
        }    
    }

    public Vector3 GetActualPos()
    {
        return Vector3.Scale(transform.position, new Vector3(1f, 0f, 1f)) + (Vector3.up * actualHeight);
    }

    public void DoHeightInput(float _height)
    {
        Quaternion cameraQuat = Quaternion.LookRotation(Vector3.Scale(playerMovement.playerCamera.transform.forward, new Vector3(1f, 0f, 1f)).normalized, Vector3.up);

        Vector3 finalInput = cameraQuat * (Vector3.up * -_height);
        finalInput = new Vector3(MathHelper.Sign(finalInput.x), MathHelper.Sign(finalInput.y), MathHelper.Sign(finalInput.z));

        Vector3 finalPos = Vector3.Scale(transform.position, new Vector3(1f, 0f, 1f)) + (Vector3.up * actualHeight) + finalInput;

        if (worldStateManager.IsInsideWorldChunk(finalPos) != null)
        {
            actualHeight = finalPos.y;
        }
    }
}
