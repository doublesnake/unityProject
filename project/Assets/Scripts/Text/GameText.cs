using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameText{

    

    public enum Text
    {
        // Instructions
        INSTRUCT_ESC,
        INSTRUCT_ENTER,
        INSTRUCT_ARROWS,
        // Main Menu
        MAIN_MENU_TITLE,
        MAIN_ARCADE_BUTTON,
        MAIN_VERSUS_BUTTON,
        MAIN_TRAINING_BUTTON,
        MAIN_OPTIONS_BUTTON,
        MAIN_ARCADE_BUTTON_DESC,
        MAIN_VERSUS_BUTTON_DESC,
        MAIN_TRAINING_BUTTON_DESC,
        MAIN_OPTIONS_BUTTON_DESC,
        // Options Menu
        OP_MENU_TITLE,
        OP_CPU_BUTTON,
        OP_ROUNDS_BUTTON,
        OP_TIME_BUTTON,
        OP_LANGUAGE_BUTTON,
        OP_CONTROLS_BUTTON,
        OP_AUDIO_BUTTON,
        OP_CPU_BUTTON_DESC,
        OP_ROUNDS_BUTTON_DESC,
        OP_TIME_BUTTON_DESC,
        OP_LANGUAGE_BUTTON_DESC,
        OP_CONTROLS_BUTTON_DESC,
        OP_AUDIO_BUTTON_DESC,
        // Control Menu
        CTRL_MENU_TITLE,
        CTRL_UP_BUTTON,
        CTRL_DOWN_BUTTON,
        CTRL_LEFT_BUTTON,
        CTRL_RIGHT_BUTTON,
        CTRL_LIGHT_BUTTON,
        CTRL_MEDIUM_BUTTON,
        CTRL_STRONG_BUTTON,
        CTRL_SPECIAL_BUTTON,
        CTRL_START_BUTTON,
        CTRL_CONFIG_BUTTON,
        CTRL_RESET_BUTTON,
        CTRL_CONFIG_PROMPTKEY,
        CTRL_RESET_BUTTON_DESC,
        CTRL_CONFIG_BUTTON_DESC,
        // Audio Menu
        AUDIO_MENU_TITLE,
        AUDIO_GAME_BUTTON,
        AUDIO_MUSIC_BUTTON,
        AUDIO_EFFECTS_BUTTON,
        AUDIO_RESET_BUTTON,
        AUDIO_RESET_BUTTON_DESC,
        AUDIO_MUSIC_BUTTON_DESC,
        AUDIO_EFFECTS_BUTTON_DESC,
        AUDIO_GAME_BUTTON_DESC
    }

    public enum Value
    {
        // Options Menu values
        OP_CPU_EASY,
        OP_CPU_MEDIUM,
        OP_CPU_HARD,
        OP_CPU_EXTREME,
        OP_ROUNDS_1,
        OP_ROUNDS_2,
        OP_ROUNDS_3,
        OP_TIME_30,
        OP_TIME_60,
        OP_TIME_99,
        OP_TIME_INF,
        OP_LANGUAGE_EN,
        OP_LANGUAGE_FR
    }

    Dictionary<string, string> element = new Dictionary<string, string>();
    Dictionary<string, string> elementValue = new Dictionary<string, string>();
    
    private static GameText instance;

    private GameText()
    {
        add_EN();
        add_FR();
    }

    public static GameText Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = new GameText();
            }
            return instance;
        }
    }
    
    public String Get(Text text)
    {
        return element[text.ToString() + Options.Instance.language.ToString()];
    }

    public String GetValue(Value text)
    {
        return elementValue[text.ToString() + Options.Instance.language.ToString()];
    }

    void add_EN()
    {
        string lang = "EN";
        
        foreach(string index in Enum.GetNames(typeof(Text)))
        {
            element.Add(index + lang, TranslationEN.get((Text)Enum.Parse(typeof(Text),index)));
        } 
        
        foreach (string index in Enum.GetNames(typeof(Value)))
        {
            elementValue.Add(index + lang, TranslationEN.get((Value)Enum.Parse(typeof(Value), index)));
        }
    }
    void add_FR()
    {
        string lang = "FR";

        foreach (string index in Enum.GetNames(typeof(Text)))
        {
            element.Add(index + lang, TranslationFR.get((Text)Enum.Parse(typeof(Text), index)));
        }

        foreach (string index in Enum.GetNames(typeof(Value)))
        {
            elementValue.Add(index + lang, TranslationFR.get((Value)Enum.Parse(typeof(Value), index)));
        }
    }

}
