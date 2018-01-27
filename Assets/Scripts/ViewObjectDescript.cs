using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewObjectDescript : MonoBehaviour {
	private GUIStyle descriptStyle = new GUIStyle();
	private bool clickedObj = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void showDescription (string descript){
		descriptStyle.fontSize = 40;
		GUI.Label(new Rect(Screen.height/2 - 20, Screen.height/2 - 20,100,100), descript, descriptStyle);
	}
		
	void OnGUI(){
		if (clickedObj) {
			showDescription ("This is a test");
		}
	}

	void OnMouseOver(){
		if (Input.GetMouseButtonDown (0)) {
			clickedObj = !clickedObj;
		}
	}
	/*
	void OnMouseExit(){
			clickedObj = false;
	}
	*/
}
