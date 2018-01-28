using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayControls : MonoBehaviour {
	private GUIStyle descriptStyle = new GUIStyle();
	private bool showContr = false;
	private bool showCred = false;
	public GameObject start;
	public GameObject controls;
	public GameObject creds;
	public GameObject quit;
	public GameObject creditImage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowControls(){
		showContr = true;

	}

	public void ShowCreds(){
		showCred = true;
	}

	public void OnGUI(){
		if (showContr) {
			descriptStyle.fontSize = 40;
			descriptStyle.normal.textColor = Color.white;
			string descript = "Use the arrow keys or";
			GUI.Label (new Rect (Screen.height / 2, Screen.height / 2 - 60, 100, 100), descript, descriptStyle);
			string descript2 = "W (up), A (left), S (down), ";
			GUI.Label (new Rect (Screen.height / 2, Screen.height / 2 - 20, 100, 100), descript2, descriptStyle);
			string descript3 = "and D (right) to move around";
			GUI.Label (new Rect (Screen.height / 2, Screen.height / 2 + 20, 100, 100), descript3, descriptStyle);
			string descript4 = "(Press any key to exit)";
			GUI.Label (new Rect (Screen.height / 2, Screen.height / 2 + 60, 100, 100), descript4, descriptStyle);
			start.SetActive (false);
			controls.SetActive (false);
			creds.SetActive (false);
			quit.SetActive (false);
			if (Input.anyKeyDown) {
				showContr = false;
			}
		} else if (showCred) {
			/*
			descriptStyle.fontSize = 40;
			descriptStyle.normal.textColor = Color.white;
			string descript = "By Andrew, Arman,";
			GUI.Label (new Rect (Screen.height / 2 , Screen.height / 2 - 60, 100, 100), descript, descriptStyle);
			string descript2 = "Joris, Nathan, and Ryan";
			GUI.Label (new Rect (Screen.height / 2, Screen.height / 2 - 20, 100, 100), descript2, descriptStyle);
			string descript3 = "(Press any key to exit)";
			GUI.Label (new Rect (Screen.height / 2, Screen.height / 2 + 60, 100, 100), descript3, descriptStyle);
			*/
			start.SetActive (false);
			creds.SetActive (false);
			quit.SetActive (false);
			creditImage.SetActive (true);
			if (Input.anyKeyDown) {
				showCred = false;
			}
		}
		else {
			creditImage.SetActive (false);
			start.SetActive (true);
			creds.SetActive (true);
			quit.SetActive (true);
		}
	}
}
