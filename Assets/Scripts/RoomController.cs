using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

    public float width;
    public float height;
    public float x1;
    public float x2;
    public float z1;
    public float z2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //float x = Mathf.Abs(x2 - x1) / 2 + x1;
        //float z = Mathf.Abs(z2 - z1) / 2 + z1;
        //this.transform.position = new Vector3(x, this.transform.position.y, z);
	}
}
