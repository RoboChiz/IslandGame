using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbitCam : MonoBehaviour
{
    private Vector2 xyValues = Vector2.zero, actualXYValues;
    public float xSpeed = 150f, ySpeed = 150f, lerpSpeed = 3f, zoomSpeed = 1f;
    private float actualRadius;

    public Transform target;
    public float radius = 6f;

    public float rotateControllerSpeed = 2f, zoomControllerSpeed = 0.2f;

    public float mouseScale = 50f, controllerScale = 50f;

    // Use this for initialization
    void Start ()
    {
        xyValues = new Vector2(-90, 45f);
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (target)
        {
            if (InputManager.controllers.Count > 0)
            {
                InputDevice inputDevice = InputManager.controllers[0];

                if (inputDevice.inputType == InputType.Keyboard)
                {
                    float xInput = Input.GetAxis("Mouse X") * mouseScale;
                    float yInput = Input.GetAxis("Mouse Y") * mouseScale;

                    //Inputs
                    xyValues.x += xInput * xSpeed * Time.deltaTime;
                    xyValues.y += yInput * ySpeed * Time.deltaTime;
                }
                else if ( InputManager.controllers[0].inputType == InputType.Xbox360)
                {
                    xyValues.y -= InputManager.controllers[0].GetInput("CameraVertical") * rotateControllerSpeed * controllerScale;
                    xyValues.x -= InputManager.controllers[0].GetInput("CameraHorizontal") * rotateControllerSpeed * controllerScale;

                    //radius += InputManager.controllers[0].GetInput("MenuVertical") * zoomControllerSpeed;
                }

                //radius -= Input.mouseScrollDelta.y * zoomSpeed;
            }

            xyValues.y = Mathf.Clamp(xyValues.y, 0f, 90f);

            actualXYValues = Vector2.Lerp(actualXYValues, xyValues, Time.deltaTime * lerpSpeed);
            actualRadius = Mathf.Lerp(actualRadius, radius, Time.deltaTime * lerpSpeed);

            Quaternion rotation = Quaternion.Euler(actualXYValues.y, actualXYValues.x, 0f);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -actualRadius);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position + Vector3.up;

        }
    }
}
