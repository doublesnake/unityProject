using UnityEngine;
using System.Collections;

using T = GameText.Text;
using V = GameText.Value;
public class TranslationEN{

    public static string get(T index)
    {
        switch(index)
        {
            // Instructions
            case T.INSTRUCT_ESC: return "Cancel/Quit";
            case T.INSTRUCT_ENTER: return "Confirm";
            case T.INSTRUCT_ARROWS: return "Navigate";
            // Main Menu
            case T.MAIN_MENU_TITLE: return "MAIN MENU";
            case T.MAIN_ARCADE_BUTTON: return "Arcade";
            case T.MAIN_VERSUS_BUTTON: return "Versus";
            case T.MAIN_TRAINING_BUTTON: return "Training";
            case T.MAIN_OPTIONS_BUTTON: return "Options";
            case T.MAIN_ARCADE_BUTTON_DESC: return "Play against CPU";
            case T.MAIN_VERSUS_BUTTON_DESC: return "Play against another player";
            case T.MAIN_TRAINING_BUTTON_DESC: return "Practice and improve your skills";
            case T.MAIN_OPTIONS_BUTTON_DESC: return "Manage game options";
            // Options Menu
            case T.OP_MENU_TITLE: return "OPTIONS";
            case T.OP_CPU_BUTTON: return "CPU Level";
            case T.OP_ROUNDS_BUTTON: return "Rounds";
            case T.OP_TIME_BUTTON: return "Time";
            case T.OP_LANGUAGE_BUTTON: return "Language";
            case T.OP_CONTROLS_BUTTON: return "Controls";
            case T.OP_AUDIO_BUTTON: return "Audio";
            case T.OP_CPU_BUTTON_DESC: return "Change CPU level";
            case T.OP_ROUNDS_BUTTON_DESC: return "Change rounds number";
            case T.OP_TIME_BUTTON_DESC: return "Change round time";
            case T.OP_LANGUAGE_BUTTON_DESC: return "Change game language";
            case T.OP_CONTROLS_BUTTON_DESC: return "Manage game controls";
            case T.OP_AUDIO_BUTTON_DESC: return "Manage game audio";
            // Control Menu
            case T.CTRL_MENU_TITLE: return "CONTROLS";
            case T.CTRL_UP_BUTTON: return "UP";
            case T.CTRL_DOWN_BUTTON: return "DOWN";
            case T.CTRL_LEFT_BUTTON: return "LEFT";
            case T.CTRL_RIGHT_BUTTON: return "RIGHT";
            case T.CTRL_LIGHT_BUTTON: return "LIGHT ATTACK";
            case T.CTRL_MEDIUM_BUTTON: return "MEDIUM ATTACK";
            case T.CTRL_STRONG_BUTTON: return "STRONG ATTACK";
            case T.CTRL_SPECIAL_BUTTON: return "SPECIAL ATTACK";
            case T.CTRL_START_BUTTON: return "START";
            case T.CTRL_CONFIG_BUTTON: return "Configuration";
            case T.CTRL_CONFIG_PROMPTKEY: return "- ENTER KEY -";
            case T.CTRL_RESET_BUTTON: return "Default values";
            case T.CTRL_RESET_BUTTON_DESC: return "Reset to default values";
            case T.CTRL_CONFIG_BUTTON_DESC: return "Configure inputs";
            // Audio Menu
            case T.AUDIO_MENU_TITLE: return "AUDIO";
            case T.AUDIO_MUSIC_BUTTON: return "Music volume";
            case T.AUDIO_EFFECTS_BUTTON: return "Sound effects volume";
            case T.AUDIO_GAME_BUTTON: return "Game volume";
            case T.AUDIO_RESET_BUTTON: return "Default values";
            case T.AUDIO_RESET_BUTTON_DESC: return "Reset to default values";
            case T.AUDIO_MUSIC_BUTTON_DESC: return "Set music volume";
            case T.AUDIO_EFFECTS_BUTTON_DESC: return "Set sound effects volume";
            case T.AUDIO_GAME_BUTTON_DESC: return "Set game volume";

            default: return "Unknown";

        }
    }
    public static string get(V index)
    {
        switch (index)
        {
            // Options Values
            case V.OP_CPU_EASY: return "Easy";
            case V.OP_CPU_MEDIUM: return "Medium";
            case V.OP_CPU_HARD: return "Hard";
            case V.OP_CPU_EXTREME: return "Extreme";
            case V.OP_ROUNDS_1: return "1";
            case V.OP_ROUNDS_2: return "2";
            case V.OP_ROUNDS_3: return "3";
            case V.OP_TIME_30: return "30";
            case V.OP_TIME_60: return "60";
            case V.OP_TIME_99: return "99";
            case V.OP_TIME_INF: return "None";
            case V.OP_LANGUAGE_EN: return "EN";
            case V.OP_LANGUAGE_FR: return "FR";



            default: return "Unknown";

        }
    }
}
