using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomInputField : UIConnection
{
    bool isLocked;
    int lockedPlayer = -1;

    public override void OnClicked(int _playerID)
    {
        if(!isLocked)
        {
            isLocked = true;
            eventSystem.SetSelectedGameObject(gameObject);
            InputManager.controllers[_playerID].localLocked = true;
            lockedPlayer = _playerID;
        }
        else
        {
            Unlock();
        }
       
    }

    public override void OnSelected()
    {
        if (GetComponent<Image>() != null)
        {
            GetComponent<Image>().color = selectedColour;
        }

        if (isLocked)
        {
            GetComponent<RectTransform>().localScale = Vector3.Lerp(GetComponent<RectTransform>().localScale, Vector3.one * 1.1f, Time.unscaledDeltaTime * 8f);
        }
        else
        {
            GetComponent<RectTransform>().localScale = Vector3.Lerp(GetComponent<RectTransform>().localScale, Vector3.one * 1.025f, Time.unscaledDeltaTime * 8f);
        }
    }

    public override void OnUnSelected()
    {
        if (GetComponent<Image>() != null)
        {
            GetComponent<Image>().color = normalColour;
        }
        GetComponent<RectTransform>().localScale = Vector3.Lerp(GetComponent<RectTransform>().localScale, Vector3.one, Time.unscaledDeltaTime * 8f);

        Unlock();
    }

    private void Unlock()
    {
        if (isLocked)
        {
            isLocked = false;
            eventSystem.SetSelectedGameObject(null);
            InputManager.controllers[lockedPlayer].localLocked = false;
            lockedPlayer = -1;
        }
    }

}

