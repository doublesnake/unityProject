using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


using T = GameText.Text;
using V = GameText.Value;
using B = OptionsMenu.Button;
using Menus;
public class OptionsMenu : MonoBehaviour
{
    public static string TITLE = "Title";
    public static string SELECTED = "Selected";
    public static string PREVIOUS = "Previous";
    public static string NEXT = "Next";

    public int selectedButton = 0;
    public Transform title;
    public Transform cpu;
    public Transform rounds;
    public Transform time;
    public Transform language;
    public Transform controls;
    public Transform audio;

    public Transform cpuValue;
    public Transform roundsValue;
    public Transform timeValue;
    public Transform languageValue;

    public GameObject menuSwitcher;
    public GameObject instructionsMenu;




    private List<Transform> groupTransform = new List<Transform>();


    public enum Button { CPU, ROUNDS, TIME, LANGUAGE, CONTROLS, AUDIO };

    public Text normal;
    public Text selected;
    public Text selectionInfo;
    public GameText gameText;
    public Options options;
    Menus.Input input;

    // Use this for initialization
    void Start()
    {
        selectedButton = 0;
        input = Menus.Input.Instance;
        options = Options.Instance;
        gameText = GameText.Instance;
        groupTransform.AddRange(new List<Transform>() { cpu, rounds, time, language, controls, audio });
        displayMenu();
    }
    void displayMenu()
    {
        cpu.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.OP_CPU_BUTTON);
        rounds.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.OP_ROUNDS_BUTTON);
        time.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.OP_TIME_BUTTON);
        language.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.OP_LANGUAGE_BUTTON);
        controls.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.OP_CONTROLS_BUTTON);
        audio.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.OP_AUDIO_BUTTON);


        title.GetComponent<Text>().text = gameText.Get(T.OP_MENU_TITLE);

        updateValues();
        updateSelection();

    }
    void updateValues()
    {
        // CPU values
        switch (options.level)
        {
            case Options.Level.EASY:
                cpuValue.GetComponent<Text>().text = gameText.GetValue(V.OP_CPU_EASY);
                break;
            case Options.Level.MEDIUM:
                cpuValue.GetComponent<Text>().text = gameText.GetValue(V.OP_CPU_MEDIUM);
                break;
            case Options.Level.HARD:
                cpuValue.GetComponent<Text>().text = gameText.GetValue(V.OP_CPU_HARD);
                break;
            case Options.Level.EXTREME:
                cpuValue.GetComponent<Text>().text = gameText.GetValue(V.OP_CPU_EXTREME);
                break;

            default:
                cpuValue.GetComponent<Text>().text = "";
                break;
        }

        // Rounds values
        switch (options.rounds)
        {
            case Options.Round.ROUND_1:
                roundsValue.GetComponent<Text>().text = gameText.GetValue(V.OP_ROUNDS_1);
                break;
            case Options.Round.ROUND_2:
                roundsValue.GetComponent<Text>().text = gameText.GetValue(V.OP_ROUNDS_2);
                break;
            case Options.Round.ROUND_3:
                roundsValue.GetComponent<Text>().text = gameText.GetValue(V.OP_ROUNDS_3);
                break;

            default:
                roundsValue.GetComponent<Text>().text = "";
                break;
        }
        // Time values
        switch (options.time)
        {
            case Options.Time.TIME_30:
                timeValue.GetComponent<Text>().text = gameText.GetValue(V.OP_TIME_30);
                break;
            case Options.Time.TIME_60:
                timeValue.GetComponent<Text>().text = gameText.GetValue(V.OP_TIME_60);
                break;
            case Options.Time.TIME_99:
                timeValue.GetComponent<Text>().text = gameText.GetValue(V.OP_TIME_99);
                break;
            case Options.Time.TIME_INF:
                timeValue.GetComponent<Text>().text = gameText.GetValue(V.OP_TIME_INF);
                break;

            default:
                timeValue.GetComponent<Text>().text = "";
                break;
        }
        // Language values
        switch (options.language)
        {
            case Options.Lang.EN:
                languageValue.GetComponent<Text>().text = gameText.GetValue(V.OP_LANGUAGE_EN);
                break;
            case Options.Lang.FR:
                languageValue.GetComponent<Text>().text = gameText.GetValue(V.OP_LANGUAGE_FR);
                break;

            default:
                languageValue.GetComponent<Text>().text = "";
                break;
        }
    }


    B getCurrentButton()
    {
        return (B)(Enum.GetValues(typeof(B))).GetValue(selectedButton);
    }
    void checkAction()
    {
        if(input.isKeyCancel())
        {
            menuSwitcher.GetComponent<MenuSwitcher>().switchBackTo(MenuSwitcher.Menu.MAIN, (int)MainMenu.Button.OPTIONS);
            return;
        }

        string[] list;
        int index;
        switch (getCurrentButton())
        {
            case B.CPU:
                list = Enum.GetNames(typeof(Options.Level));
                index = Array.IndexOf(list, options.level.ToString());
                if (input.isKeyLeft())
                {
                    if (index == 0) options.level = (Options.Level)(Enum.Parse(typeof(Options.Level), (string)list.GetValue(list.Length - 1)));
                    else
                    {
                        options.level = (Options.Level)(Enum.Parse(typeof(Options.Level),(string)list.GetValue(index-1)));
                    } 
                }
                if (input.isKeyRight())
                {
                    if (index == list.Length - 1) options.level = (Options.Level)(Enum.Parse(typeof(Options.Level), (string)list.GetValue(0)));
                    else
                    {
                        options.level = (Options.Level)(Enum.Parse(typeof(Options.Level), (string)list.GetValue(index + 1)));
                    } 
                }
                switch(options.level)
                {
                    case Options.Level.EASY:
                        cpuValue.GetComponent<Text>().text = gameText.GetValue(V.OP_CPU_EASY);
                        break;
                    case Options.Level.MEDIUM:
                        cpuValue.GetComponent<Text>().text = gameText.GetValue(V.OP_CPU_MEDIUM);
                        break;
                    case Options.Level.HARD:
                        cpuValue.GetComponent<Text>().text = gameText.GetValue(V.OP_CPU_HARD);
                        break;
                    case Options.Level.EXTREME:
                        cpuValue.GetComponent<Text>().text = gameText.GetValue(V.OP_CPU_EXTREME);
                        break;

                    default:
                        cpuValue.GetComponent<Text>().text = "";
                        break;
                }
                break;
            case B.ROUNDS:
                list = Enum.GetNames(typeof(Options.Round));
                index = Array.IndexOf(list, options.rounds.ToString());
                if (input.isKeyLeft())
                {
                    if (index == 0) options.rounds = (Options.Round)(Enum.Parse(typeof(Options.Round), (string)list.GetValue(list.Length - 1)));
                    else
                    {
                        options.rounds = (Options.Round)(Enum.Parse(typeof(Options.Round), (string)list.GetValue(index - 1)));
                    }
                }
                if (input.isKeyRight())
                {
                    if (index == list.Length - 1) options.rounds = (Options.Round)(Enum.Parse(typeof(Options.Round), (string)list.GetValue(0)));
                    else
                    {
                        options.rounds = (Options.Round)(Enum.Parse(typeof(Options.Round), (string)list.GetValue(index + 1)));
                    }
                }
                switch(options.rounds)
                {
                    case Options.Round.ROUND_1:
                        roundsValue.GetComponent<Text>().text = gameText.GetValue(V.OP_ROUNDS_1);
                        break;
                    case Options.Round.ROUND_2:
                        roundsValue.GetComponent<Text>().text = gameText.GetValue(V.OP_ROUNDS_2);
                        break;
                    case Options.Round.ROUND_3:
                        roundsValue.GetComponent<Text>().text = gameText.GetValue(V.OP_ROUNDS_3);
                        break;

                    default:
                        roundsValue.GetComponent<Text>().text = "";
                        break;
                }
                break;
            case B.TIME:
                list = Enum.GetNames(typeof(Options.Time));
                index = Array.IndexOf(list, options.time.ToString());
                if (input.isKeyLeft())
                {
                    if (index == 0) options.time = (Options.Time)(Enum.Parse(typeof(Options.Time), (string)list.GetValue(list.Length - 1)));
                    else
                    {
                        options.time = (Options.Time)(Enum.Parse(typeof(Options.Time), (string)list.GetValue(index - 1)));
                    }
                }
                if (input.isKeyRight())
                {
                    if (index == list.Length - 1) options.time = (Options.Time)(Enum.Parse(typeof(Options.Time), (string)list.GetValue(0)));
                    else
                    {
                        options.time = (Options.Time)(Enum.Parse(typeof(Options.Time), (string)list.GetValue(index + 1)));
                    }
                }
                switch (options.time)
                {
                    case Options.Time.TIME_30:
                        timeValue.GetComponent<Text>().text = gameText.GetValue(V.OP_TIME_30);
                        break;
                    case Options.Time.TIME_60:
                        timeValue.GetComponent<Text>().text = gameText.GetValue(V.OP_TIME_60);
                        break;
                    case Options.Time.TIME_99:
                        timeValue.GetComponent<Text>().text = gameText.GetValue(V.OP_TIME_99);
                        break;
                    case Options.Time.TIME_INF:
                        timeValue.GetComponent<Text>().text = gameText.GetValue(V.OP_TIME_INF);
                        break;

                    default:
                        timeValue.GetComponent<Text>().text = "";
                        break;
                }
                break;
            case B.LANGUAGE:
                list = Enum.GetNames(typeof(Options.Lang));
                index = Array.IndexOf(list, options.language.ToString());
                if (input.isKeyLeft())
                {
                    if (index == 0) options.language = (Options.Lang)(Enum.Parse(typeof(Options.Lang), (string)list.GetValue(list.Length - 1)));
                    else
                    {
                        options.language = (Options.Lang)(Enum.Parse(typeof(Options.Lang), (string)list.GetValue(index - 1)));
                    }
                }
                if (input.isKeyRight())
                {
                    if (index == list.Length - 1) options.language = (Options.Lang)(Enum.Parse(typeof(Options.Lang), (string)list.GetValue(0)));
                    else
                    {
                        options.language = (Options.Lang)(Enum.Parse(typeof(Options.Lang), (string)list.GetValue(index + 1)));
                    }
                }
                switch (options.language)
                {
                    case Options.Lang.EN:
                        languageValue.GetComponent<Text>().text = gameText.GetValue(V.OP_LANGUAGE_EN);
                        break;
                    case Options.Lang.FR:
                        languageValue.GetComponent<Text>().text = gameText.GetValue(V.OP_LANGUAGE_FR);
                        break;

                    default:
                        languageValue.GetComponent<Text>().text = "";
                        break;
                }
                displayMenu();
                instructionsMenu.GetComponent<Instructions>().update();

                break;
            
            case B.CONTROLS:
                if(input.isKeyEnter())
                {
                    reset();
                    menuSwitcher.GetComponent<MenuSwitcher>().switchTo(MenuSwitcher.Menu.CONTROLS);
                }
                break;
            case B.AUDIO:
                if (input.isKeyEnter())
                {
                    reset();
                    menuSwitcher.GetComponent<MenuSwitcher>().switchTo(MenuSwitcher.Menu.AUDIO);
                }
                break;
            default:break;
        }
        updateValues();
    }

    void reset()
    {
        selectedButton = 0;
        displayMenu();
    }

    public void Init()
    {
        reset();
    }
    public void Init(int button)
    {
        selectedButton = button;
        displayMenu();
    }
    void updateInfo()
    {
        switch (getCurrentButton())
        {
            case B.CPU: selectionInfo.text = gameText.Get(T.OP_CPU_BUTTON_DESC); break;
            case B.ROUNDS: selectionInfo.text = gameText.Get(T.OP_ROUNDS_BUTTON_DESC); break;
            case B.TIME: selectionInfo.text = gameText.Get(T.OP_TIME_BUTTON_DESC); break;
            case B.LANGUAGE: selectionInfo.text = gameText.Get(T.OP_LANGUAGE_BUTTON_DESC); break;
            case B.CONTROLS: selectionInfo.text = gameText.Get(T.OP_CONTROLS_BUTTON_DESC); break;
            case B.AUDIO: selectionInfo.text = gameText.Get(T.OP_AUDIO_BUTTON_DESC); break;

            default: selectionInfo.text = ""; break;
        }
    }
    void resetFont()
    {
        foreach(Transform t in groupTransform)
        {
            t.Find(TITLE).GetComponent<Text>().font = normal.font;
            t.Find(TITLE).GetComponent<Text>().color = normal.color;
            t.Find(TITLE).GetComponent<Text>().fontSize = normal.fontSize;
        }
    }
    void setSelected(Transform t)
    {
        t.Find(TITLE).GetComponent<Text>().font = selected.font;
        t.Find(TITLE).GetComponent<Text>().color = selected.color;
        t.Find(TITLE).GetComponent<Text>().fontSize = selected.fontSize;
    }
    void updateSelection()
    {
        resetFont();
        cpu.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        rounds.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        time.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        language.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        controls.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        audio.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        switch (getCurrentButton())
        {
            case B.CPU: cpu.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(cpu); break;
            case B.ROUNDS: rounds.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(rounds); break;
            case B.TIME: time.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(time); break;
            case B.LANGUAGE: language.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(language); break;
            case B.CONTROLS: controls.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(controls); break;
            case B.AUDIO: audio.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(audio); break;

            default: selectionInfo.text = ""; break;
        }
        updateInfo();
    }
    void Update()
    {
        if(input.isKeyUp())
        {
            if (selectedButton == 0) return;
            selectedButton--;
            updateSelection();
        }
        if (input.isKeyDown())
        {
            if (selectedButton == Enum.GetNames(typeof(B)).Length-1) return;
            selectedButton++;
            updateSelection();
        }
        if (input.isKeyLeft() || input.isKeyRight() || input.isKeyEnter() || input.isKeyCancel())
        {
            checkAction();
        }
    }
}
