using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

using T = GameText.Text;
using B = MainMenu.Button;
public class MainMenu : MonoBehaviour
{
    public static string TITLE = "Title";
    public static string SELECTED = "Selected";

    public int selectedButton = 0;
    public Transform arcade;
    public Transform versus;
    public Transform training;
    public Transform options;
    private List<Transform> groupTransform = new List<Transform>();

    public enum Button { ARCADE, VERSUS, TRAINING, OPTIONS };

    public Text normal;
    public Text selected;
    public Text selectionInfo;
    public GameText gameText;


    public GameObject menuSwitcher;
    Menus.Input input;

    // Use this for initialization
    void Start()
    {
        selectedButton = 0;
        input = Menus.Input.Instance;
        gameText = GameText.Instance;
        groupTransform.AddRange(new List<Transform>(){arcade,versus,training,options});
        displayMenu();
    }
    void displayMenu()
    {
        arcade.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.MAIN_ARCADE_BUTTON);
        versus.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.MAIN_VERSUS_BUTTON);
        training.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.MAIN_TRAINING_BUTTON);
        options.Find(TITLE).GetComponent<Text>().text = gameText.Get(T.MAIN_OPTIONS_BUTTON);
        updateSelection();

    }

    B getCurrentButton()
    {
        return (B)(Enum.GetValues(typeof(B))).GetValue(selectedButton);
    }
    void updateInfo()
    {
        switch (getCurrentButton())
        {
            case B.ARCADE: selectionInfo.text = gameText.Get(T.MAIN_ARCADE_BUTTON_DESC); break;
            case B.VERSUS: selectionInfo.text = gameText.Get(T.MAIN_VERSUS_BUTTON_DESC); break;
            case B.TRAINING: selectionInfo.text = gameText.Get(T.MAIN_TRAINING_BUTTON_DESC); break;
            case B.OPTIONS: selectionInfo.text = gameText.Get(T.MAIN_OPTIONS_BUTTON_DESC); break;

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
        arcade.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        versus.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        training.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        options.Find(TITLE).Find(SELECTED).gameObject.SetActive(false);
        switch (getCurrentButton())
        {
            case B.ARCADE: arcade.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(arcade); break;
            case B.VERSUS: versus.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(versus); break;
            case B.TRAINING: training.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(training); break;
            case B.OPTIONS: options.Find(TITLE).Find(SELECTED).gameObject.SetActive(true); setSelected(options); break;

            default: selectionInfo.text = ""; break;
        }
        updateInfo();
    }

    void checkAction()
    {
        switch (getCurrentButton())
        {
            case B.ARCADE: break;
            case B.VERSUS: break;
            case B.TRAINING:  break;
            case B.OPTIONS: menuSwitcher.GetComponent<MenuSwitcher>().switchTo(MenuSwitcher.Menu.OPTIONS); break;

            default: break;
        }
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

    void reset()
    {
        selectedButton = 0;
        displayMenu();
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
        if(input.isKeyEnter())
        {
            checkAction();
        }
    }
}
