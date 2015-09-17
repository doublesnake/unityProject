using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


using T = GameText.Text;
using V = GameText.Value;
using B = ControlBox.Button;
using Menus;
public class ControlBox : MonoBehaviour
{
    public static string TITLE = "Title";
    public static string SELECTED = "Selected";
    public static string SELECTED_ICON = "Selection_icon";

    public bool isActive;
    public int selectedButton = 0;
    public int selectedInputvalue = 0;
    public Controls.Layout actualLayout;
    public Controls.Layout tempLayout;

    public Transform title;

    #region Buttons/Values Transform
    public Transform resetValues;
    public Transform config;

    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform light;
    public Transform medium;
    public Transform strong;
    public Transform special;
    public Transform start;

    public Transform up_value;
    public Transform down_value;
    public Transform left_value;
    public Transform right_value;
    public Transform light_value;
    public Transform medium_value;
    public Transform strong_value;
    public Transform special_value;
    public Transform start_value;

    #endregion
    
    public GameObject controlsMenu;

    private List<Transform> ctrlbuttons = new List<Transform>();
    private List<Transform> configButtons = new List<Transform>();
    private List<Transform> configValues = new List<Transform>();

    private Controls.InputSet tempInputs = new Controls.InputSet();
    public enum PlayerControl { P1, P2 };


    public enum Mode { CONFIGURATION, NAVIGATION };
    public enum Button { CONFIG, RESET };

    public Text normal;
    public Text disabled;
    public Text selectionInfo;

    public PlayerControl player;
    public Mode mode;

    public GameText gameText; // access to game text
    public Options options; // access to options
    Menus.Input input; // access to inputs

    /** BEHAVIOUR FUNCTIONS **/
    #region Behaviour functions


    // At Init...
    void Start()
    {
        selectedButton = 0;
        mode = Mode.NAVIGATION;
        input = Menus.Input.Instance;
        options = Options.Instance;
        gameText = GameText.Instance;

        ctrlbuttons.AddRange(new List<Transform>() { config, resetValues });
        configButtons.AddRange(new List<Transform>() { up, down, left, right, light, medium, strong, special, start });
        configValues.AddRange(new List<Transform>() { up_value, down_value, left_value, right_value, light_value, medium_value, strong_value, special_value, start_value});
        displayMenu();
        if (player == PlayerControl.P2) { clearSelection(); isActive = false; } //disable ControlBox of P2
    }

    // At Each frame...
    void Update()
    {
        if (!isActive) return;
        switch (mode)
        {
            case Mode.NAVIGATION:
                if (input.isKeyUp())
                {
                    if (selectedButton == 0) return;
                    selectedButton--;
                    updateSelection();
                }
                if (input.isKeyDown())
                {
                    if (selectedButton == Enum.GetNames(typeof(B)).Length - 1) return;
                    selectedButton++;
                    updateSelection();
                }
                if (input.isKeyLeft() || input.isKeyRight())
                {
                    if ((player == PlayerControl.P1) && input.isKeyRight())
                    {
                        //leave Box to Box 2
                        controlsMenu.GetComponent<ControlsMenu>().switchTo(PlayerControl.P2);
                    }
                    if ((player == PlayerControl.P2) && input.isKeyLeft())
                    {
                        //leave Box to Box 1
                        controlsMenu.GetComponent<ControlsMenu>().switchTo(PlayerControl.P1);
                    }
                }
                if (input.isKeyCancel())
                {
                    //leave Menu
                    controlsMenu.GetComponent<ControlsMenu>().Quit();
                }
                if (input.isKeyEnter())
                {
                    if (getCurrentButton() == Button.CONFIG)
                    {
                        // go to config mode
                        mode = Mode.CONFIGURATION;
                        setConfigDisplay(true);
                    }
                    if (getCurrentButton() == Button.RESET)
                    {
                        //Reset config
                        resetInputs();
                        displayValues();
                    }
                }
                break;
            case Mode.CONFIGURATION:

                configValues[selectedInputvalue].GetComponent<Text>().text = gameText.Get(T.CTRL_CONFIG_PROMPTKEY);
                if (input.isKeyCancel())
                {
                    // Cancel configuration and go back to navigation mode
                    displayValues();
                    setConfigDisplay(false);
                    mode = Mode.NAVIGATION;
                }
                else if (UnityEngine.Input.anyKeyDown)
                {
                    KeyCode key = KeyCode.None;
                    foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                     {
                         if (UnityEngine.Input.GetKeyDown(kcode))
                         {
                             key = kcode;
                             break;
                         }
                     }
                    configValues[selectedInputvalue].GetComponent<Text>().text = key.ToString();
                    tempInputs.inputs[(Controls.InputSet.Button)selectedInputvalue] = key;
                    if(selectedInputvalue == getControls().current.inputs.Count-1)
                    {
                        getControls().setInputs(tempInputs);
                        setConfigDisplay(false);
                        mode = Mode.NAVIGATION;
                    }
                    else
                    {
                        selectedInputvalue++;
                        updateConfigSelection();
                    }
                }
                break;

        }
    }


    #endregion

    /** DISPLAY **/
    #region Display functions


    // Display all elements
    void displayMenu()
    {
        displayButtons();
        displayValues();
        title.GetComponent<Text>().text = gameText.Get(T.CTRL_MENU_TITLE);
        updateSelection();

    }

    // Display button values
    void displayButtons()
    {
        // Menu buttons
        config.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.CTRL_CONFIG_BUTTON);
        resetValues.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.CTRL_RESET_BUTTON);

        //Input buttons
        up.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.CTRL_UP_BUTTON);
        down.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.CTRL_DOWN_BUTTON);
        left.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.CTRL_LEFT_BUTTON);
        right.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.CTRL_RIGHT_BUTTON);
        light.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.CTRL_LIGHT_BUTTON);
        medium.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.CTRL_MEDIUM_BUTTON);
        strong.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.CTRL_STRONG_BUTTON);
        special.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.CTRL_SPECIAL_BUTTON);
        start.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.CTRL_START_BUTTON);
    }
    // Display button values
    void displayValues()
    {
        // Input values
        up_value.GetComponent<Text>().text = getControls().current.inputs[Controls.InputSet.Button.UP].ToString();
        down_value.GetComponent<Text>().text = getControls().current.inputs[Controls.InputSet.Button.DOWN].ToString();
        left_value.GetComponent<Text>().text = getControls().current.inputs[Controls.InputSet.Button.LEFT].ToString();
        right_value.GetComponent<Text>().text = getControls().current.inputs[Controls.InputSet.Button.RIGHT].ToString();
        light_value.GetComponent<Text>().text = getControls().current.inputs[Controls.InputSet.Button.LIGHT].ToString();
        medium_value.GetComponent<Text>().text = getControls().current.inputs[Controls.InputSet.Button.MEDIUM].ToString();
        strong_value.GetComponent<Text>().text = getControls().current.inputs[Controls.InputSet.Button.STRONG].ToString();
        special_value.GetComponent<Text>().text = getControls().current.inputs[Controls.InputSet.Button.SPECIAL].ToString();
        start_value.GetComponent<Text>().text = getControls().current.inputs[Controls.InputSet.Button.START].ToString();

    }


    #endregion

    /** CONFIGURATION MODE **/
    #region Configuration mode functions


    // Set the configuration mode display
    public void setConfigDisplay(bool value)
    {
        selectedInputvalue = 0;
        if (value)
        {
            foreach (Transform t in ctrlbuttons)
            {
                setDisabledFont(t.Find(TITLE));
            }
            foreach (Transform t in configButtons)
            {
                setNormalFont(t.Find(TITLE));
            }
            foreach (Transform t in configValues)
            {
                setNormalFont(t);
            }
            updateConfigSelection();
        }
        else
        {
            foreach (Transform t in ctrlbuttons)
            {
                setNormalFont(t.Find(TITLE));
            }
            foreach (Transform t in configButtons)
            {
                setDisabledFont(t.Find(TITLE));
            }
            foreach (Transform t in configValues)
            {
                setDisabledFont(t);
            }
            clearConfigSelection();
        }
    }

    // Update selection display of the current button
    void updateConfigSelection()
    {
        foreach (Transform t in configButtons)
        {
            t.Find(SELECTED).gameObject.SetActive(false);
        }
        configButtons[selectedInputvalue].Find(SELECTED).gameObject.SetActive(true);
    }

    // Remove selection display on configuration mode
    void clearConfigSelection()
    {
        foreach (Transform t in configButtons)
        {
            t.Find(SELECTED).gameObject.SetActive(false);
        }
    }


    #endregion

    /** UTILITIES **/
    #region Utility functions


    // Remove selection display on navigation mode
    void clearSelection()
    {
        foreach (Transform t in ctrlbuttons)
        {
            t.Find(SELECTED).gameObject.SetActive(false);
        }
    }
    
    // Set normal font
    public void setNormalFont(Transform t)
    {
        t.GetComponent<Text>().color = normal.color;
    }

    // Set disabled font
    public void setDisabledFont(Transform t)
    {
        t.GetComponent<Text>().color = disabled.color;
    }

    // Update info panel's text
    void updateInfo()
    {
        switch (getCurrentButton())
        {
            case B.CONFIG: selectionInfo.text = gameText.Get(T.CTRL_CONFIG_BUTTON_DESC); break;
            case B.RESET: selectionInfo.text = gameText.Get(T.CTRL_RESET_BUTTON_DESC); break;

            default: selectionInfo.text = ""; break;
        }
    }

    // Set the current button selected
    void updateSelection()
    {
        config.Find(SELECTED).gameObject.SetActive(false);
        resetValues.Find(SELECTED).gameObject.SetActive(false);
        switch (getCurrentButton())
        {
            case B.CONFIG: config.Find(SELECTED).gameObject.SetActive(true); break;
            case B.RESET: resetValues.Find(SELECTED).gameObject.SetActive(true); break;

            default: selectionInfo.text = ""; break;
        }
        updateInfo();
    }

    // Get the current button
    B getCurrentButton()
    {
        return (B)(Enum.GetValues(typeof(B))).GetValue(selectedButton);
    }

    // Return the ControlsBox's controls
    Controls getControls()
    {
        if (player == PlayerControl.P2)
            return options.controlsP2;
        else
            return options.controlsP1;
    }

    // Change selection to the given button
    public void setSelected(int selection)
    {
        selectedButton = selection;
        updateSelection();
    }

    // Enable/Disable the ControlBox
    public void setActive(bool value)
    {
        setConfigDisplay(false);
        isActive = value;
        if(value)
        {
            updateSelection();
        }
        else
        {
            clearSelection();
        }
    }

    // Reset the ControlBox
    public void reset(int selection)
    {
        mode = Mode.NAVIGATION;
        selectedButton = selection;
        isActive = true;
        displayMenu();
    }

    // Reset the controls with the default layout values
    void resetInputs()
    {
        getControls().resetLayout();
        //refresh config window
    }


    #endregion
}