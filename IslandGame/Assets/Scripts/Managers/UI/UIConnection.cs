using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIConnection : MonoBehaviour
{
    public UIConnection OnUp, OnDown, OnLeft, OnRight;
    public bool priority;

    public abstract void OnSelected();
    public abstract void OnUnSelected();

}
