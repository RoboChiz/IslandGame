using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIConnection : MonoBehaviour
{
    protected UIConnectionManager myManager;
    public UIConnection OnUp, OnDown, OnLeft, OnRight;
    public bool priority;

    public abstract void OnSelected();
    public abstract void OnUnSelected();
    public abstract void OnClicked(int _playerID);

    protected static EventSystem eventSystem;

    protected static Color normalColour = new Color(0.7f, 0.7f, 0.7f, 0.588f);
    protected static Color hoverColour = new Color(1f, 1f, 1f, 0.588f);
    protected static Color selectedColour = new Color(1f, 1f, 1f, 0.588f);

    public void Start()
    {
        myManager = FindObjectOfType<UIConnectionManager>();
        myManager.AddConnector(this);
    }

    public void Update()
    {
        if(eventSystem == null)
        {
            eventSystem = FindObjectOfType<EventSystem>();
        }
    }

    void OnDestroy()
    {
        if(myManager)
        {
            myManager.RemoveConnector(this);
        }
    }
}
