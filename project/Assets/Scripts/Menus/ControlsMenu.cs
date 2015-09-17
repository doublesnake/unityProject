using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Menus;

public class ControlsMenu : MonoBehaviour {

    public GameObject controlBox_P1;
    public GameObject controlBox_P2;
    public GameObject menuSwitcher;

	// Use this for initialization
	void Start () {
        controlBox_P1.GetComponent<ControlBox>().setActive(true);
        controlBox_P2.GetComponent<ControlBox>().setActive(false);
	}
	
    public void switchTo(ControlBox.PlayerControl player)
    {
        if (player == ControlBox.PlayerControl.P1)
        {

            controlBox_P2.GetComponent<ControlBox>().setActive(false);
            controlBox_P1.GetComponent<ControlBox>().setSelected(controlBox_P2.GetComponent<ControlBox>().selectedButton);
            controlBox_P1.GetComponent<ControlBox>().setActive(true);
        }
        else if (player == ControlBox.PlayerControl.P2)
        {

            controlBox_P1.GetComponent<ControlBox>().setActive(false);
            controlBox_P2.GetComponent<ControlBox>().setSelected(controlBox_P1.GetComponent<ControlBox>().selectedButton);
            controlBox_P2.GetComponent<ControlBox>().setActive(true);
        }

    }

    public void Init()
    {
        controlBox_P1.GetComponent<ControlBox>().reset(0);
        controlBox_P2.GetComponent<ControlBox>().reset(0);
        controlBox_P2.GetComponent<ControlBox>().setActive(false);
    }
    public void Quit()
    {
        menuSwitcher.GetComponent<MenuSwitcher>().switchBackTo(MenuSwitcher.Menu.OPTIONS, (int)OptionsMenu.Button.CONTROLS);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
