using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistScript : MonoBehaviour {
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis ("Vertical");
		anim.SetFloat ("Speed", move);
		if (Input.GetKeyDown (KeyCode.A))
			anim.Play ("Walking");
	}
}
