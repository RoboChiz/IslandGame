using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConnectionManager : MonoBehaviour {

    public int currentUI;

    private UIConnection currentUIConnection;
    private List<UIConnection> allConnectors;

	// Use this for initialization
	void Start ()
    {
        allConnectors = new List<UIConnection>();
    }

    public void AddConnector(UIConnection _uiConnection)
    {
        allConnectors.Add(_uiConnection);
    }

    public void RemoveConnector(UIConnection _uiConnection)
    {
        allConnectors.Remove(_uiConnection);
    }

    // Update is called once per frame
    void Update ()
    {       
        currentUI = allConnectors.Count;

        //Unselect Option if it becomes unavailable
        if(currentUIConnection != null && !currentUIConnection.isActiveAndEnabled)
        {
            currentUIConnection = null;
        }

        bool mouseOnAnything = false;
        foreach (UIConnection uiConnector in allConnectors)
        {
            //Do Open and Close if Button
            if (uiConnector.isActiveAndEnabled && uiConnector.GetComponent<RectTransform>() != null)
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

                    currentUIConnection.OnClicked();
                }

                if (currentUIConnection.GetComponent<InputField>() != null && mouseOnAnything && InputManager.GetClick(0))
                {
                    currentUIConnection.OnClicked();
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
