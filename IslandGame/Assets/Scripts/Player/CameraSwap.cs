using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwap : MonoBehaviour
{
    public Camera orbitCamera, isoCamera;

    OrbitCam orbitCam;

    private bool useIsoCamera = true;
    private bool invertHorizontal = false, invertVertical = true;

	// Use this for initialization
	void Start ()
    {
        orbitCam = orbitCamera.GetComponent<OrbitCam>();

        //Get Current camera mode from save
        UpdateCameraState();
    }

    public void SetCameraState(bool _state)
    {
        useIsoCamera = _state;
        UpdateCameraState();
    }

    public void SetInvertHorizontal(Toggle _toggle) { SetInvertHorizontal(_toggle.isOn); }
    public void SetInvertHorizontal(bool _value)
    {
        invertHorizontal = _value;
        UpdateCameraState();
    }

    public void SetInvertVertical(Toggle _toggle) { SetInvertVertical(_toggle.isOn); }
    public void SetInvertVertical (bool _value)
    {
        invertVertical = _value;
        UpdateCameraState();
    }

    private void UpdateCameraState()
    {
        if (useIsoCamera)
        {
            orbitCamera.gameObject.SetActive(false);
            isoCamera.gameObject.SetActive(true);

            FindObjectOfType<PlayerMovement>().playerCamera = isoCamera;
        }
        else
        {
            orbitCamera.gameObject.SetActive(true);
            isoCamera.gameObject.SetActive(false);

            FindObjectOfType<PlayerMovement>().playerCamera = orbitCamera;
        }

        //Send Invert Controls to OrbitCamera
        orbitCam.invertHorizontal = invertHorizontal;
        orbitCam.invertVertical = invertVertical;
    }

    public bool GetCameraState() { return useIsoCamera; }
    public bool GetInvertHorizontal() { return invertHorizontal; }
    public bool GetInvertVertical() { return invertVertical; }

}
