using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class IsoCam : MonoBehaviour
{
    //Changeable Values
    private Vector2 xyValues = Vector2.zero, actualXYValues;
    private float size = 5f, actualSize;

    public Transform target;

    public float xSpeed = 150f, lerpSpeed = 3f;

    public float mouseScale = 50f, controllerScale = 50f;

    public float[] focusToggleDepths;
    private int currentDepth = 1;

    private Camera myCamera;

    public bool lockCamera = false;

    // Use this for initialization
    void Start()
    {
        xyValues = new Vector2(37f, 45f);
        myCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            //Snap Round
            if(xyValues.y > 360f && actualXYValues.y > 180f)
            {
                actualXYValues.y -= 360f;
            }
            else if (xyValues.y < 0f && actualXYValues.y < 180f)
            {
                actualXYValues.y += 360f;
            }

            xyValues.y = MathHelper.NumClamp(xyValues.y, 0f, 360f);
            actualXYValues = Vector2.Lerp(actualXYValues, xyValues, Time.deltaTime * lerpSpeed);

            Quaternion rotation = Quaternion.Euler(actualXYValues.x, actualXYValues.y, 0f);
            transform.rotation = rotation;

            //Set Forward
            Vector3 localForward = rotation * Vector3.forward;

            //Do Sizing
            size = focusToggleDepths[currentDepth];
            actualSize = Mathf.Lerp(actualSize, size, Time.deltaTime * lerpSpeed);

            transform.position = target.position - (localForward * actualSize);

            //myCamera.orthographicSize = actualSize;

            //User Inputs
            if (!lockCamera && InputManager.controllers.Count > 0)
            {
                InputDevice inputDevice = InputManager.controllers[0];

                if (inputDevice.GetButtonWithDelay("RotateLeft", 0.5f, Time.deltaTime))
                {
                    xyValues.y += 90f;
                }

                if (inputDevice.GetButtonWithDelay("RotateRight", 0.5f, Time.deltaTime))
                {
                    xyValues.y -= 90f;
                }

                float focus = inputDevice.GetInputWithDelay("FocusDir", 0.5f, Time.deltaTime, 0.25f);

                if(focus == 0)
                {
                    focus = Input.GetAxis("Mouse Scrollwheel");
                }

                if (Mathf.Abs(focus) >= 0.25f)
                {
                    currentDepth = Mathf.Clamp(currentDepth + MathHelper.Sign(focus), 0, focusToggleDepths.Length - 1);
                }

                if (inputDevice.GetButtonWithLock("Focus"))
                {
                    currentDepth = MathHelper.NumClamp(currentDepth + 1, 0, focusToggleDepths.Length);
                }
            }
        }
    }
}
