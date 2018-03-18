using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : UIConnection
{
    private static Color normalColour = new Color(0.7f, 0.7f, 0.7f, 0.588f);
    private static Color hoverColour = new Color(1f, 1f, 1f, 0.588f);
    private static Color selectedColour = new Color(1f, 1f, 1f, 0.588f);

    public void Clicked ()
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

