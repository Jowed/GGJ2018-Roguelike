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
		StartCoroutine (Wait5 ());

	}
	IEnumerator Wait5()
	{
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene ("Main");
	}
}
