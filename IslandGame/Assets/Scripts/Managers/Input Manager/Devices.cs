using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputIgnoreList
{
    public static readonly List<string> ignoreList = new List<string>() { "Throttle", "Brake", "SteerLeft", "SteerRight", "Drift", "Item", "RearView"};
}

//Predefined Controllers
public class KeyboardDevice : InputDevice
{
    //Constructor
    public KeyboardDevice(int _joystickID) : base(0) { }

    //Tell it we're a Keyboard
    public override InputType inputType { get { return InputType.Keyboard; } }

    //Return a static default control layout
    public override ControlLayout defaultLayout { get { return myDefault; } }
    public static ControlLayout myDefault = new ControlLayout(InputType.Keyboard, "Default",
        new Dictionary<string, List<ControlName>>()
        {
            //Menus
            {"Leave", new List<ControlName>() {new ControlName("backspace", false) } },
            {"MenuHorizontal", new List<ControlName>() {new ControlName("a-", false), new ControlName("d+", false) } },
            {"MenuVertical", new List<ControlName>() {new ControlName("w-", false), new ControlName("s+", false) } },
            {"Submit", new List<ControlName>() {new ControlName("return", false) } },
            {"Cancel", new List<ControlName>() {new ControlName("escape", false) } },

            //Player Controls           
            {"MovementHorizontal", new List<ControlName>() {new ControlName("a-", false), new ControlName("d+", false) } },
            {"MovementVertical", new List<ControlName>() {new ControlName("w-", false), new ControlName("s+", false) } },
            {"Focus", new List<ControlName>() {new ControlName("f", true) } },           
        }
    );
}
public class XBox360Device : InputDevice
{
    //Constructor
    public XBox360Device(int _joystickID) : base(_joystickID) { }

    //Tell it we're a 360 controller
    public override InputType inputType { get { return InputType.Xbox360; } }

    //Return a static default control layout
    public override ControlLayout defaultLayout { get { return myDefault; } }
    public static ControlLayout myDefault = new ControlLayout(InputType.Xbox360, "Default",
        new Dictionary<string, List<ControlName>>()
        {
            //Menu Controls
            {"Leave", new List<ControlName>() {new ControlName(Back, false) } },
            {"MenuHorizontal", new List<ControlName>() {new ControlName(LeftStickHori,true), new ControlName(DPadHori, true) } },
            {"MenuVertical", new List<ControlName>() {new ControlName(LeftStickVert,true), new ControlName(DPadVert + "*", true) } },
            {"Submit", new List<ControlName>() {new ControlName(Start, false), new ControlName(A, false) } },
            {"Cancel", new List<ControlName>() {new ControlName(B, false) } },

            //Player Controls
            {"MovementHorizontal", new List<ControlName>() {new ControlName(LeftStickHori,true)} },
            {"MovementVertical", new List<ControlName>() {new ControlName(LeftStickVert,true)} }, 
            {"CameraHorizontal", new List<ControlName>() {new ControlName(RightStickHori, true) } },
            {"CameraVertical", new List<ControlName>() {new ControlName(RightStickVert, true) } },
            {"Focus", new List<ControlName>() {new ControlName(LS, true) } },
        
        }
    );

    //Const Mappings to avoid knowing exact button names
    public const string LeftStickHori = "AxisX";
    public const string LeftStickVert = "AxisY";

    public const string LT = "Axis3-";
    public const string RT = "Axis3+";

    public const string RightStickHori = "Axis4";
    public const string RightStickVert = "Axis5";

    public const string DPadHori = "Axis6";
    public const string DPadVert = "Axis7";

    public const string A = "button 0";
    public const string B = "button 1";
    public const string X = "button 2";
    public const string Y = "button 3";

    public const string LB = "button 4";
    public const string RB = "button 5";

    public const string Back = "button 6";
    public const string Start = "button 7";

    public const string LS = "button 8";
    public const string RS = "button 9";
}