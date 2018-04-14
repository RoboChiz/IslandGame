using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : UIConnection
{
    public override void OnClicked(int _playerID)
    {      

    }

    public override void OnSelected()
    {
        if (GetComponent<Image>() != null)
        {
            GetComponent<Image>().color = selectedColour;
        }

        GetComponent<RectTransform>().localScale = Vector3.Lerp(GetComponent<RectTransform>().localScale, Vector3.one * 1.1f, Time.unscaledDeltaTime * 8f);
    }

    public override void OnUnSelected()
    {
        if (GetComponent<Image>() != null)
        {
            GetComponent<Image>().color = normalColour;
        }
        GetComponent<RectTransform>().localScale = Vector3.Lerp(GetComponent<RectTransform>().localScale, Vector3.one, Time.unscaledDeltaTime * 8f);
    }
}

