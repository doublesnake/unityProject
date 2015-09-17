using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


using T = GameText.Text;
using V = GameText.Value;
using B = AudioMenu.Button;
using Menus;
public class AudioMenu : MonoBehaviour
{
    public static string TITLE = "Title";
    public static string SELECTED = "Selected";
    public static string PREVIOUS = "Previous";
    public static string NEXT = "Next";

    public int selectedButton = 0;
    public Transform title;
    public Transform game;
    public Transform music;
    public Transform effects;
    public Transform resetButton;

    public Transform game_value;
    public Transform music_value;
    public Transform effects_value;
    public GameObject menuSwitcher;


    private List<Transform> buttons = new List<Transform>();


    public enum Button { GAME, MUSIC, EFFECTS, RESET};

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
        buttons.AddRange(new List<Transform>() { game, music, effects, resetButton});
        displayMenu();
    }
    void displayMenu()
    {
        game.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.AUDIO_GAME_BUTTON);
        music.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.AUDIO_MUSIC_BUTTON);
        effects.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.AUDIO_EFFECTS_BUTTON);
        resetButton.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.AUDIO_RESET_BUTTON);
        title.GetComponent<Text>().text = gameText.Get(T.AUDIO_MENU_TITLE);
        updateValues();
        updateSelection();

    }
    void updateValues()
    {
        game_value.GetComponent<Text>().text = options.audio.game.value.ToString();
        music_value.GetComponent<Text>().text = options.audio.music.value.ToString();
        effects_value.GetComponent<Text>().text = options.audio.effects.value.ToString();

        if (options.audio.game.isMax()) game_value.Find(NEXT).gameObject.SetActive(false);
        else game_value.Find(NEXT).gameObject.SetActive(true);

        if (options.audio.game.isMin()) game_value.Find(PREVIOUS).gameObject.SetActive(false);
        else game_value.Find(PREVIOUS).gameObject.SetActive(true);

        if (options.audio.music.isMax()) music_value.Find(NEXT).gameObject.SetActive(false);
        else music_value.Find(NEXT).gameObject.SetActive(true);

        if (options.audio.music.isMin()) music_value.Find(PREVIOUS).gameObject.SetActive(false);
        else music_value.Find(PREVIOUS).gameObject.SetActive(true);


        if (options.audio.effects.isMax()) effects_value.Find(NEXT).gameObject.SetActive(false);
        else effects_value.Find(NEXT).gameObject.SetActive(true);

        if (options.audio.effects.isMin()) effects_value.Find(PREVIOUS).gameObject.SetActive(false);
        else effects_value.Find(PREVIOUS).gameObject.SetActive(true);
       
    }



    B getCurrentButton()
    {
        return (B)(Enum.GetValues(typeof(B))).GetValue(selectedButton);
    }
    void checkAction()
    {
        if(input.isKeyCancel())
        {
            menuSwitcher.GetComponent<MenuSwitcher>().switchBackTo(MenuSwitcher.Menu.OPTIONS, (int)OptionsMenu.Button.AUDIO);
            return;
        }

        switch (getCurrentButton())
        {
            case B.GAME:
                if (input.isKeyLeft())
                {
                    options.audio.game.decrease();
                }
                if (input.isKeyRight())
                {
                    options.audio.game.increase();
                }
                break;
            case B.MUSIC:
                if (input.isKeyLeft())
                {
                    options.audio.music.decrease();
                }
                if (input.isKeyRight())
                {
                    options.audio.music.increase();
                }
                break;
            case B.EFFECTS:
                if (input.isKeyLeft())
                {
                    options.audio.effects.decrease();
                }
                if (input.isKeyRight())
                {
                    options.audio.effects.increase();
                }
                break;
            case B.RESET:
                if (input.isKeyEnter())
                {
                    options.audio.effects.reset();
                    options.audio.effects.reset();
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
            case B.GAME: selectionInfo.text = gameText.Get(T.AUDIO_GAME_BUTTON_DESC); break;
            case B.MUSIC: selectionInfo.text = gameText.Get(T.AUDIO_MUSIC_BUTTON_DESC); break;
            case B.EFFECTS: selectionInfo.text = gameText.Get(T.AUDIO_EFFECTS_BUTTON_DESC); break;
            case B.RESET: selectionInfo.text = gameText.Get(T.AUDIO_RESET_BUTTON_DESC); break;

            default: selectionInfo.text = ""; break;
        }
    }
    void resetFont()
    {
        foreach(Transform t in buttons)
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
        game.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        music.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        effects.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        resetButton.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        switch (getCurrentButton())
        {
            case B.GAME: game.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(game); break;
            case B.MUSIC: music.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(music); break;
            case B.EFFECTS: effects.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(effects); break;
            case B.RESET: resetButton.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(resetButton); break;

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
