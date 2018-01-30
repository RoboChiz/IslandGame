using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float tiptoeSpeedPercent = 0.3333f, walkSpeedPercent = 0.6666f, moveSpeed = 4f,
        brakeTime = 0.5f, acceleration = 2f, 
        expectedSpeed = 0f, jumpVelocity = 10f, 
        turnSpeed = 5f, swimSpeed = 2f;

    private const float tipPercent = 0.25f, walkPercent = 0.5f;
    public Camera playerCamera;

    public Vector3 lastDirection { get; private set; }
    public Quaternion lastCameraQuat { get; private set; }

    public Vector3 pointDirection { get; private set; }

    public bool lockMovements = false, inWater= false, isJumping = false;
    public bool isFalling = false, jumpLock = false;

    public float isFallCheck = 1f;

    Queue<float> lastRotAngles = new Queue<float>();

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        //Get Correct Camera Transform
        Vector3 cameraFoward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
        Quaternion cameraQuat = Quaternion.LookRotation(cameraFoward, Vector3.up);

        if (InputManager.controllers.Count > 0)
        {
            InputDevice inputDevice = InputManager.controllers[0];

            //Get Inputs
            float hori = inputDevice.GetInput("MovementHorizontal");
            float verti = inputDevice.GetInput("MovementVertical");
            bool jump = false;

            if(!lockMovements)
            {
                jump = inputDevice.GetButtonWithLock("Jump");
            }

            //Move the character in the direction of the camera
            float maxInput = Mathf.Max(Mathf.Abs(hori), Mathf.Abs(verti));

            Vector3 input = new Vector3(hori, 0f, -verti).normalized;

            Vector2 inputAmount = new Vector2(hori, verti);
            float moveAmount = Mathf.Clamp(inputAmount.magnitude, 0f, 1f);

            if (moveAmount == 0 || lockMovements)
            {
                float cacceleration = -expectedSpeed / brakeTime;
                expectedSpeed += (cacceleration * Time.fixedDeltaTime);

                if (Mathf.Abs(expectedSpeed) <= 0.1f)
                    expectedSpeed = 0;

                input = lastDirection;
            }
            else
            {
                //Get Max Movement Speed
                float maxSpeed = moveSpeed;
                if(inWater)
                {
                    maxSpeed = swimSpeed;
                }
              
                //Adjust speed depending on amount of input
                if(maxInput < tipPercent)
                {
                    maxSpeed *= tiptoeSpeedPercent; 
                }
                else if (maxInput < walkPercent)
                {
                    maxSpeed *= walkSpeedPercent;
                }

                expectedSpeed += (moveAmount * acceleration) * Time.fixedDeltaTime;
                expectedSpeed = Mathf.Clamp(expectedSpeed, 0f, maxSpeed);

                if (maxInput > 0.01f)
                {
                    lastDirection = input;
                    lastCameraQuat = cameraQuat;
                }
            }        

            Vector3 finalInput = cameraQuat * input * expectedSpeed;

            pointDirection = (cameraQuat * input).normalized;

            Vector3 accelerationVec = (Vector3.Scale(finalInput, new Vector3(1f, 0f, 1f)) - Vector3.Scale(rigidbody.velocity, new Vector3(1f, 0f, 1f))) / Time.fixedDeltaTime;
            rigidbody.AddForce(accelerationVec, ForceMode.Acceleration);

            //Do Jump
            if(jump && (!isFalling || inWater) && !jumpLock)
            {
                rigidbody.velocity += new Vector3(0f, jumpVelocity, 0f);
                jumpLock = true;
                isJumping = true;
            }
        }

        RaycastHit hit;
        isFalling = !Physics.Raycast(transform.position, Vector3.down, out hit, isFallCheck, ~(LayerMask.GetMask("Water") | LayerMask.GetMask("Ignore Raycast")), QueryTriggerInteraction.UseGlobal);

        if(rigidbody.velocity.y <= 0f && jumpLock)
        {
            jumpLock = false;
            isJumping = false;
        }

        //Turn to Face Look Direction
        if (lastDirection != Vector3.zero)
        {
            //Do a bunch of angle checks, if we're moving alot presume we're spinning
            Quaternion finalRot = Quaternion.LookRotation(lastCameraQuat * lastDirection, Vector3.up);
            float angle = Quaternion.Angle(transform.rotation, finalRot), averageAngle = 0f;
            float currentTurnSpeed = turnSpeed;

            if(lastRotAngles.Count >= 40f)
            {
                lastRotAngles.Dequeue();
            } 
            lastRotAngles.Enqueue(angle);
      
            foreach(float value in lastRotAngles)
            {
                averageAngle += value;
            }
            averageAngle /= lastRotAngles.Count;

            if (averageAngle > 50f)
            {
                currentTurnSpeed *= 3f;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, finalRot, Time.fixedDeltaTime * currentTurnSpeed);
        }

        // -- Do Animation --

        animator.SetFloat("Speed", expectedSpeed);
        animator.SetBool("IsFalling", isFalling);
    }

    public void GetFromLoad
    (
        Vector3 _lastDirection, 
        Vector3 _pointDirection, 
        float _expectedSpeed,
        Vector3 _lastCameraQuat
    )
    {
        lastDirection = _lastDirection;
        pointDirection = _pointDirection;
        expectedSpeed = _expectedSpeed;
        lastCameraQuat = Quaternion.Euler(_lastCameraQuat);
    }
}
