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
            {"Pause", new List<ControlName>() {new ControlName("escape", false) } },
            {"BuildMode", new List<ControlName>() {new ControlName("x", false) } },

            //Player Controls           
            {"MovementHorizontal", new List<ControlName>() {new ControlName("a-", false), new ControlName("d+", false) } },
            {"MovementVertical", new List<ControlName>() {new ControlName("w-", false), new ControlName("s+", false) } },
            {"Jump", new List<ControlName>() {new ControlName("space", false) } },
            {"MovementHeight", new List<ControlName>() {new ControlName("left shift-", false), new ControlName("left ctrl+", false) } },
            {"Focus", new List<ControlName>() {new ControlName("f", false) } },
            {"RotateLeft", new List<ControlName>() {new ControlName("q", false) } },
            {"RotateRight", new List<ControlName>() {new ControlName("e", false) } },
            {"Create", new List<ControlName>() {new ControlName("k", false) } },
            {"Delete", new List<ControlName>() {new ControlName("l", false) } },
            {"Rotate", new List<ControlName>() {new ControlName("z" + "-", false) } },

            {"Item1", new List<ControlName>() {new ControlName("1", false) } },
            {"Item2", new List<ControlName>() {new ControlName("2", false) } },
            {"Item3", new List<ControlName>() {new ControlName("3", false) } },
            {"Item4", new List<ControlName>() {new ControlName("4", false) } },
            {"Item5", new List<ControlName>() {new ControlName("5", false) } },
            {"Item6", new List<ControlName>() {new ControlName("6", false) } },
            {"Item7", new List<ControlName>() {new ControlName("7", false) } },
            {"Item8", new List<ControlName>() {new ControlName("8", false) } },
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
            {"Submit", new List<ControlName>() {new ControlName(A, false) } },
            {"Cancel", new List<ControlName>() {new ControlName(B, false) } },
            {"Pause", new List<ControlName>() {new ControlName(Start, false) } },
            {"BuildMode", new List<ControlName>() {new ControlName(X, false) } },

            //Player Controls
            {"MovementHorizontal", new List<ControlName>() {new ControlName(LeftStickHori,true)} },
            {"MovementVertical", new List<ControlName>() {new ControlName(LeftStickVert,true)} },
            {"Jump", new List<ControlName>() {new ControlName(A, false) } },

            {"CameraHorizontal", new List<ControlName>() {new ControlName(RightStickHori, true) } },
            {"CameraVertical", new List<ControlName>() {new ControlName(RightStickVert, true) } },

            {"MovementHeight", new List<ControlName>() {new ControlName(RightStickVert, true) } },
            {"Focus", new List<ControlName>() {new ControlName(LS, false) } },
            {"RotateLeft", new List<ControlName>() {new ControlName(RightStickHori + "-", true) } },
            {"RotateRight", new List<ControlName>() {new ControlName(RightStickHori + "+", true) } },
            {"Create", new List<ControlName>() {new ControlName(A, false) } },
            {"Delete", new List<ControlName>() {new ControlName(B, false) } },
            {"ItemSwitch", new List<ControlName>() {new ControlName(LB + "-", false), new ControlName(RB + "+", false) } },
            {"Rotate", new List<ControlName>() {new ControlName(LT, true), new ControlName(RT, true) } },
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