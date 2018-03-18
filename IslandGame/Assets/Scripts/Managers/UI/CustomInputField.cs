using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomInputField : UIConnection
{
    bool justUnlocked;
    public void Start()
    {
        GetComponent<InputField>().onEndEdit.AddListener(delegate (string _string) { eventSystem.SetSelectedGameObject(null); justUnlocked = true; });
    }

    public override void OnClicked()
    {
        if(justUnlocked)
        {
            justUnlocked = false;
            return;
        }

        eventSystem.SetSelectedGameObject(gameObject);
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

