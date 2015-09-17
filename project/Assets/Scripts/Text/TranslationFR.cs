using UnityEngine;
using System.Collections;

using T = GameText.Text;
using V = GameText.Value;
public class TranslationFR{

    public static string get(T index)
    {
        switch (index)
        {
            // Instructions
            case T.INSTRUCT_ESC: return "Annuler/Quitter";
            case T.INSTRUCT_ENTER: return "Confirmer";
            case T.INSTRUCT_ARROWS: return "Naviguer";
            // Main Menu
            case T.MAIN_MENU_TITLE: return "MENU PRINCIPAL";
            case T.MAIN_ARCADE_BUTTON: return "Arcade";
            case T.MAIN_VERSUS_BUTTON: return "Versus";
            case T.MAIN_TRAINING_BUTTON: return "Entraînement";
            case T.MAIN_OPTIONS_BUTTON: return "Options";
            case T.MAIN_ARCADE_BUTTON_DESC: return "Affronte l'ordinateur";
            case T.MAIN_VERSUS_BUTTON_DESC: return "Affronte d'autres joueurs";
            case T.MAIN_TRAINING_BUTTON_DESC: return "Entraîne toi et améliore tes compétences";
            case T.MAIN_OPTIONS_BUTTON_DESC: return "Gère les options de jeu";
            // Options Menu
            case T.OP_MENU_TITLE: return "OPTIONS";
            case T.OP_CPU_BUTTON: return "Niveau IA";
            case T.OP_ROUNDS_BUTTON: return "Rounds";
            case T.OP_TIME_BUTTON: return "Temps";
            case T.OP_LANGUAGE_BUTTON: return "Langue";
            case T.OP_CONTROLS_BUTTON: return "Contrôles";
            case T.OP_AUDIO_BUTTON: return "Audio";
            case T.OP_CPU_BUTTON_DESC: return "Change le niveau de l'ordinateur";
            case T.OP_ROUNDS_BUTTON_DESC: return "Change le nombre de manches";
            case T.OP_TIME_BUTTON_DESC: return "Change la durée du round";
            case T.OP_LANGUAGE_BUTTON_DESC: return "Change la langue du jeu";
            case T.OP_CONTROLS_BUTTON_DESC: return "Gére les contrôles du jeu";
            case T.OP_AUDIO_BUTTON_DESC: return "Gére les sons du jeu";
            // Control Menu
            case T.CTRL_MENU_TITLE: return "CONTRÔLES";
            case T.CTRL_UP_BUTTON: return "HAUT";
            case T.CTRL_DOWN_BUTTON: return "BAS";
            case T.CTRL_LEFT_BUTTON: return "GAUCHE";
            case T.CTRL_RIGHT_BUTTON: return "DROITE";
            case T.CTRL_LIGHT_BUTTON: return "COUP FAIBLE";
            case T.CTRL_MEDIUM_BUTTON: return "COUP MOYEN";
            case T.CTRL_STRONG_BUTTON: return "COUP FORT";
            case T.CTRL_SPECIAL_BUTTON: return "COUP SPECIAL";
            case T.CTRL_START_BUTTON: return "START";
            case T.CTRL_CONFIG_BUTTON: return "Configuration";
            case T.CTRL_CONFIG_PROMPTKEY: return "- ENTRER UNE TOUCHE -";
            case T.CTRL_RESET_BUTTON: return "Valeurs par défaut";
            case T.CTRL_RESET_BUTTON_DESC: return "Réinitialiser par défaut";
            case T.CTRL_CONFIG_BUTTON_DESC: return "Configure les touches";
            // Audio Menu
            case T.AUDIO_MENU_TITLE: return "AUDIO";
            case T.AUDIO_MUSIC_BUTTON: return "Volume de la musique";
            case T.AUDIO_EFFECTS_BUTTON: return "Volumes des bruitages";
            case T.AUDIO_GAME_BUTTON: return "Volume du jeu";
            case T.AUDIO_RESET_BUTTON: return "Valeurs par défaut";
            case T.AUDIO_RESET_BUTTON_DESC: return "Réinitialiser par défaut";
            case T.AUDIO_MUSIC_BUTTON_DESC: return "Règle le volume de la musique";
            case T.AUDIO_EFFECTS_BUTTON_DESC: return "Règle le volume des bruitages";
            case T.AUDIO_GAME_BUTTON_DESC: return "Règle le volume du jeu";


            default: return "Inconnu";

        }
    }
    public static string get(V index)
    {
        switch (index)
        {
            // Options Values
            case V.OP_CPU_EASY: return "Facile";
            case V.OP_CPU_MEDIUM: return "Moyen";
            case V.OP_CPU_HARD: return "Difficile";
            case V.OP_CPU_EXTREME: return "Extrême";
            case V.OP_ROUNDS_1: return "1";
            case V.OP_ROUNDS_2: return "2";
            case V.OP_ROUNDS_3: return "3";
            case V.OP_TIME_30: return "30";
            case V.OP_TIME_60: return "60";
            case V.OP_TIME_99: return "99";
            case V.OP_TIME_INF: return "Aucun";
            case V.OP_LANGUAGE_EN: return "EN";
            case V.OP_LANGUAGE_FR: return "FR";



            default: return "Unknown";

        }
    }

}
