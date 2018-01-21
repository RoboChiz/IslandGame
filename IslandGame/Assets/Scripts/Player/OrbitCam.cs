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
    private float radius = 6f;

    public float mouseScale = 50f, controllerScale = 50f;

    private bool focused = true;
    public float focusedRadius = 19.8f, unfocusedRadius = 35.0f;

    //Set by Camera Swap
    public bool invertHorizontal = false, invertVertical = true;

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

                    xyValues.x += xInput * xSpeed * Time.deltaTime * (invertHorizontal ? -1f : 1f);
                    xyValues.y += yInput * ySpeed * Time.deltaTime * (invertVertical ? -1f : 1f);
                }
                else if ( InputManager.controllers[0].inputType == InputType.Xbox360)
                {                  
                    xyValues.x -= InputManager.controllers[0].GetInput("CameraHorizontal") * ySpeed * controllerScale * Time.deltaTime * (invertHorizontal ? -1f : 1f);
                    xyValues.y += InputManager.controllers[0].GetInput("CameraVertical") * xSpeed * controllerScale * Time.deltaTime * (invertVertical ? -1f : 1f);
                }

                if(inputDevice.GetButtonWithLock("Focus"))
                {
                    focused = !focused;
                }
            }

            xyValues.y = Mathf.Clamp(xyValues.y, 0f, 90f);

            radius = focused ? focusedRadius : unfocusedRadius;

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
