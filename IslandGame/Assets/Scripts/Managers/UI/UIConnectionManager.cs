using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConnectionManager : MonoBehaviour {

    public int currentUI;

    private UIConnection currentUIConnection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        UIConnection[] list = FindObjectsOfType<UIConnection>();
        currentUI = list.Length;

        //Unselect Option if it becomes unavailable
        if(currentUIConnection != null && !currentUIConnection.isActiveAndEnabled)
        {
            currentUIConnection = null;
        }

        bool mouseOnAnything = false;
        foreach (UIConnection uiConnector in list)
        {
            //Do Open and Close if Button
            if (uiConnector.isActiveAndEnabled && uiConnector.GetComponent<RectTransform>() != null && (uiConnector.GetComponent<Button>() != null || uiConnector.GetComponent<Toggle>() != null))
            {
                //Set as default
                if(currentUIConnection == null && uiConnector.priority)
                {
                    currentUIConnection = uiConnector;
                }
         
                if (Cursor.visible)
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(uiConnector.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
                    {
                        currentUIConnection = uiConnector;
                        mouseOnAnything = true;
                    }
                }
                
                if(currentUIConnection == uiConnector)
                {
                    uiConnector.OnSelected();                  
                }
                else
                {
                    uiConnector.OnUnSelected();
                }
            }
        }

        //Do Input
        if (InputManager.controllers.Count > 0)
        {
            InputDevice playerOne = InputManager.controllers[0];

            if (currentUIConnection != null)
            {
                if (playerOne.GetRawButtonWithLock("Submit"))
                {
                    if (currentUIConnection.GetComponent<Button>() != null)
                    {
                        currentUIConnection.GetComponent<Button>().onClick.Invoke();
                    }

                    if (currentUIConnection.GetComponent<Toggle>() != null)
                    {
                        currentUIConnection.GetComponent<Toggle>().onValueChanged.Invoke(!currentUIConnection.GetComponent<Toggle>().isOn);
                    }
                }
                if (!mouseOnAnything)
                {
                    int hori = playerOne.GetRawIntInputWithDelay("MenuHorizontal", 0.25f, Time.unscaledDeltaTime);
                    int vert = playerOne.GetRawIntInputWithDelay("MenuVertical", 0.25f, Time.unscaledDeltaTime);

                    //Navigation
                    if (hori > 0 && currentUIConnection.OnRight != null)
                    {
                        currentUIConnection = currentUIConnection.OnRight;
                    }

                    if (hori < 0 && currentUIConnection.OnLeft != null)
                    {
                        currentUIConnection = currentUIConnection.OnLeft;
                    }

                    if (vert < 0 && currentUIConnection.OnUp != null)
                    {
                        currentUIConnection = currentUIConnection.OnUp;
                    }

                    if (vert > 0 && currentUIConnection.OnDown != null)
                    {
                        currentUIConnection = currentUIConnection.OnDown;
                    }
                }
            }
        }
    }
}
