﻿using System.Collections;
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

    private bool focused = true;
    public float focusedSize = 5f, unfocusedSize = 15f;

    private Camera myCamera;

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
            transform.position = target.position - (localForward * 50f);

            //Do Sizing
            size = focused ? focusedSize : unfocusedSize;
            actualSize = Mathf.Lerp(actualSize, size, Time.deltaTime * lerpSpeed);

            myCamera.orthographicSize = actualSize;

            //User Inputs
            if (InputManager.controllers.Count > 0)
            {
                InputDevice inputDevice = InputManager.controllers[0];

                if (inputDevice.GetButtonWithLock("RotateLeft"))
                {
                    xyValues.y += 90f;
                }

                if (inputDevice.GetButtonWithLock("RotateRight"))
                {
                    xyValues.y -= 90f;
                }

                if (inputDevice.GetButtonWithLock("Focus"))
                {
                    focused = !focused;
                }
            }
        }
    }
}