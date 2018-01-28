using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadIntoMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine (Wait1 ());

	}
	IEnumerator Wait1()
	{
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene ("Main");
	}
}
