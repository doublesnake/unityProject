using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Input.GetKey ("backspace");
	}

	public void gradeBtn(){
		Application.LoadLevel ("SelectPlayerScene");
	}

	public void recessBtn(){
		Application.LoadLevel ("RecessModeScene");
	}

	public void settingBtn(){
		Application.LoadLevel ("SettingScene");
	}
}
