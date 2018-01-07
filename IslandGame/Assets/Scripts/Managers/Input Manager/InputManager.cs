using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Input Manager V3
//Created by Robo_Chiz
public class InputManager : MonoBehaviour
{
    public const int maxPlayers = 4, maxJoysticks = 11;

    public enum InputState { Locked, LockedShowing, MeetAmount, Any, AnyMinusKeyboard }
    public static InputState inputState { get; private set; }

    public enum ToggleState { Locked, AnyButOne, Any }
    public static ToggleState toggleState { get; private set; }

    public InputState localInput;
    public ToggleState localToggle;

    public static int targetAmount { get; private set; }

    public static List<InputDevice> controllers;
    public static List<ControlLayout> allAvailableConfigs;

    public static List<List<ControlLayout>> configsPerType;

    public static bool mouseLock;

    // Use this for initialization
    void Awake()
    {
        //Create the list of controllers for the Game
        controllers = new List<InputDevice>();

        //Load all of the available configs
        LoadAllControllerLayouts();

#if UNITY_EDITOR
        inputState = localInput;
        toggleState = localToggle;
#else
        inputState = InputState.Locked;
#endif

    }

    void Update()
    {
        //Update Local Vals for Inspector
        localInput = inputState;
        localToggle = toggleState;

        //Hide Mouse if it's not used
        if (Cursor.lockState == CursorLockMode.Locked || ((Input.anyKey || (controllers.Count > 0 && (controllers[0].GetRawInput("MenuHorizontal") != 0 || controllers[0].GetRawInput("MenuVertical") != 0))) && !Input.GetMouseButton(0)))
        {
            Cursor.visible = false;
        }

        if (Cursor.lockState != CursorLockMode.Locked && (Input.GetMouseButton(0) || Input.GetAxis("Mouse X") != 0f || Input.GetAxis("Mouse Y") != 0f))
            Cursor.visible = true;

        //Check for new inputs
        if (inputState != InputState.Locked && inputState != InputState.LockedShowing)
        {
            if (controllers.Count < maxPlayers)
            {
                //Get a list of Joysticks in Use
                List<int> inputsInUse = new List<int>();
                foreach (InputDevice controller in controllers)
                    inputsInUse.Add(controller.joystickID);

                //Get Keyboard Inputs
                if (inputState != InputState.AnyMinusKeyboard)
                {
                    //If Keyboard is not already in
                    if (!inputsInUse.Contains(0))
                    {
                        bool recivedKey = false;

                        for (int i = 1; i < 300; i++)
                        {
                            if (Input.GetKey((KeyCode)i))
                            {
                                recivedKey = true;
                                break;
                            }
                        }

                        if (recivedKey || (Input.GetMouseButton(0) && controllers.Count == 0))
                        {
                            controllers.Add(new KeyboardDevice(0));
                        }
                    }
                }

                //Get Joystick Inputs
                for (int i = 1; i <= maxJoysticks; i++)
                {
                    //If Joystick is not in use
                    if (!inputsInUse.Contains(i))
                    {
                        //If we get an input
                        if (RecieveInput(i))
                        {
                            if (inputState == InputState.Locked || inputState == InputState.LockedShowing)
                            {
                                //Do Locked GUI Flash
                            }
                            else
                            {
                                //Detect Type of Input Device (Or don't)                      

                                //Add this Joystick to the Controllers
                                controllers.Add(new XBox360Device(i));
                            }
                        }
                    }
                }
            }      
        }

        //Controller Removing 
        foreach (InputDevice controller in controllers.ToArray())
        {
            //Do Controller Removing
            if (controller.GetButton("Leave"))
            {
                if (inputState != InputState.Locked && inputState != InputState.LockedShowing)
                    controller.killing = true;
                else
                {
                    //Do Locked GUI Flash
                }
            }
        }

        //Controller Unlocking and Toggling
        //bool firstController = true;
        foreach (InputDevice controller in controllers.ToArray())
        {
            //Kill Controller
            if(controller.killing)
            {
                controllers.Remove(controller);
            }

            //Do InputLock unlocking
            if (controller.inputLock != "")
            {
                if (controller.GetRawInput(controller.inputLock) == 0f)
                    controller.inputLock = "";
            }

            /*
            //Make sure Controller has correct Layout
            if (!controller.toggle && controller.currentLayout != configsPerType[(int)controller.inputType][controller.currentSelection])
                controller.SetLayout(configsPerType[(int)controller.inputType][controller.currentSelection]);

            //Do Controller Toggling
            if (toggleState == ToggleState.Any || (toggleState == ToggleState.AnyButOne && !firstController))
                if (controller.GetButtonWithLockForToggle("Toggle"))
                {
                    //Show Layout Selection
                    controller.toggle = !controller.toggle;
                }

            //Do Toggle Menu Controls
            if (controller.toggle && controller.boxHeight == 450f)
            {
                bool submit = controller.GetButtonWithLockForToggle("Submit");
                bool cancel = controller.GetButtonWithLockForToggle("Cancel");
                int vert = controller.GetRawIntInputWithLockForToggle("MenuVertical");

                if (vert != 0)
                    controller.currentSelection = MathHelper.NumClamp(controller.currentSelection + vert, 0, InputManager.configsPerType[(int)controller.inputType].Count);

                if (submit)
                {
                    controller.SetLayout(InputManager.configsPerType[(int)controller.inputType][controller.currentSelection]);
                    controller.toggle = false;
                }

                if (cancel)
                    controller.toggle = false;
            }
           
            //Set to false at end of first loop
            firstController = false;
            */
        }

        //Toggle States
        if (toggleState == ToggleState.Locked)
        {
            foreach (InputDevice device in controllers)
                device.toggle = false;
        }

        if (toggleState == ToggleState.AnyButOne && controllers.Count >= 1)
        {
            controllers[0].toggle = false;
        }
    }

    void OnGUI()
    {
        GUI.depth = -100;

        //If we're not in the locked state, render active controllers
        if (controllers.Count > 0)
        {
            //Foreach Controller
            for(int i = 0; i < controllers.Count; i++)
            {
                //Get the Controller
                InputDevice device = controllers[i];

                GUI.Box(new Rect(10 + (i * 110), 10, 100, 25), i.ToString() + ": " + device.inputType.ToString());
            }
        }
    }

    //Checks if there is any Joystick Input
    public bool RecieveInput(int joyStick)
    {
        int startVal = 330 + (joyStick * 20);

        for (int i = startVal; i < startVal + 20; i++)
        {
            if (Input.GetKey((KeyCode)i))
                return true;
        }

        return false;
    }

    //Checks for a click
    public static bool GetClick()
    {
        if (!mouseLock && Input.GetMouseButtonDown(0))
        {
            mouseLock = true;
            return true;
        }

        return false;
    }

    //Removes the specified controller
    public static void RemoveController(int id)
    {
        foreach (InputDevice controller in controllers.ToArray())
        {
            if (controller.joystickID == id)
            {
                controller.killing = true;
                break;
            }
        }
    }

    //Remove all controllers minus the first
    public static void RemoveAllButOneController()
    {
        if (controllers.Count > 1)
        {
            controllers.RemoveRange(1, controllers.Count - 1);
        }
    }

    //Loads all Controller Layouts
    public static void LoadAllControllerLayouts()
    {
        allAvailableConfigs = new List<ControlLayout>();

        //Get string from Player Prefs
        string toLoad = PlayerPrefs.GetString("ControllerLayouts", "");

        if (toLoad != "")
        {
            //Split the string into each Controller Layout
            string[] inputs = toLoad.Split(';');

            foreach (string inputData in inputs)
            {
                //Parse the controller layout from the string. If an exception is thrown ignore that string
               // try
                //{
                    ControlLayout cl = ControlLayout.Parse(inputData);
                    allAvailableConfigs.Add(cl);
                /*}
                catch (Exception err)
                {
                    Debug.LogError(err.Message);
                }*/
            }         
        }

        SetAllControllerLayouts(allAvailableConfigs);
    }

    public static void SetAllControllerLayouts(List<ControlLayout> newList)
    {
        allAvailableConfigs = newList;

        //Create a second list where layouts are grouped by type
        configsPerType = new List<List<ControlLayout>>();

        int maxAmount = Enum.GetValues(typeof(InputType)).Length;

        for (int i = 0; i < maxAmount; i++)
            configsPerType.Add(new List<ControlLayout>());

        //Add Defaults
        configsPerType[0].Add(KeyboardDevice.myDefault);
        configsPerType[1].Add(XBox360Device.myDefault);

        foreach (ControlLayout controlLayout in allAvailableConfigs)
            configsPerType[(int)controlLayout.inputType].Add(controlLayout);
    }

    //Saves all Controller Layouts
    public static void SaveAllControllerLayouts()
    {
        string toSave = "";

        foreach (ControlLayout cl in allAvailableConfigs)
        {
            if (toSave != "")
                toSave += ";";

            toSave += cl.ToString();
        }

        PlayerPrefs.SetString("ControllerLayouts", toSave);
    }

    public static void SetInputState(InputState newState)
    {
        //Set the amount we need to meet
        if (newState == InputState.MeetAmount)
            targetAmount = controllers.Count;

        inputState = newState;
    }

    public static void SetToggleState(ToggleState newState)
    {
        toggleState = newState;
    }

    public static string GetXboxInput(ref bool usesAxes)
    {
        //Array of all Axes to check
        string[] possibleControls = new string[] { "AxisX", "AxisY", "Axis3", "Axis4", "Axis5", "Axis6", "Axis7"};

        for (int i = 1; i <= InputManager.maxJoysticks; i++)
        {
            for (int j = 0; j < possibleControls.Length; j++)
            {
                float value = Input.GetAxisRaw("Joystick" + i + possibleControls[j]);
                if (value > 0)
                {
                    usesAxes = true;
                    return possibleControls[j] + "+";
                }
                else if (value < 0)
                {
                    usesAxes = true;
                    return possibleControls[j] + "-";
                }
            }
        }

        //Check for Button
        for (int j = 0; j < 20; j++)
        {
            if(Input.GetKey((KeyCode)(330 + j)))
            {
                usesAxes = false;
                string returnVal = ((KeyCode)(330 + j)).ToString();

                //Remove Joystick X from string
                returnVal = returnVal.Remove(0, returnVal.IndexOf("Button"));

                //Make Button Lowercase
                returnVal = returnVal.Replace("Button", "button");

                //Add a space after button 
                returnVal = returnVal.Insert(returnVal.IndexOf("Button") + 7, " ");

                return returnVal;

            }
        }

        return "";
    }

}
