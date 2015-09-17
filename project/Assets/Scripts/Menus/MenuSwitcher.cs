using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Menus;

public class MenuSwitcher : MonoBehaviour {

    public enum Menu { MAIN, OPTIONS, CONTROLS, AUDIO }
    Menu currentMenu;

    public GameObject main;
    public GameObject options;
    public GameObject controls;
    public GameObject audio;

    private Dictionary<Menu, GameObject> groupMenu = new Dictionary<Menu, GameObject>();
	// Use this for initialization
	void Start () {
        groupMenu.Add(Menu.MAIN, main);
        groupMenu.Add(Menu.OPTIONS, options);
        groupMenu.Add(Menu.CONTROLS, controls);
        groupMenu.Add(Menu.AUDIO, audio);

	}
	
    public void switchTo(Menu menu)
    { 
        foreach(GameObject m in groupMenu.Values)
        {
            m.SetActive(false);
        }
        groupMenu[menu].SetActive(true);
        switch(menu)
        {
            case Menu.CONTROLS:
                controls.GetComponent<ControlsMenu>().Init();
                break;
            case Menu.MAIN:
                main.GetComponent<MainMenu>().Init();
                break;
            case Menu.OPTIONS:
                options.GetComponent<OptionsMenu>().Init();
                break;
            case Menu.AUDIO:
                audio.GetComponent<AudioMenu>().Init();
                break;
            default:
                break;
        }
    }
    public void switchBackTo(Menu menu, int button)
    {
        foreach (GameObject m in groupMenu.Values)
        {
            m.SetActive(false);
        }
        groupMenu[menu].SetActive(true);
        switch (menu)
        {
            case Menu.CONTROLS:
                controls.GetComponent<ControlsMenu>().Init();
                break;
            case Menu.MAIN:
                main.GetComponent<MainMenu>().Init(button);
                break;
            case Menu.OPTIONS:
                options.GetComponent<OptionsMenu>().Init(button);
                break;
            case Menu.AUDIO:
                audio.GetComponent<AudioMenu>().Init();
                break;
            default:
                break;
        }

    }
	// Update is called once per frame
	void Update () {
	
	}
}
