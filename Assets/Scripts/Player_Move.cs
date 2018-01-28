using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

    public float movementSpeed = 1.0f;
    public float mouseSpeed = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = new Vector3(); //(0,0,0)
        //float CharacterSpeed = 10.0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            dir.z += 1.0f;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            dir.x -= 1.0f;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            dir.z -= 1.0f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            dir.x += 1.0f;
        else if (Input.GetKey(KeyCode.Tab))
            dir.y -= 1.0f;
        else if (Input.GetKey(KeyCode.Space))
            dir.y += 1.0f;

        dir.Normalize();
        transform.Translate(dir * movementSpeed * Time.deltaTime);
        float x = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSpeed;
        //float z = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSpeed;
        
        //transform.position += new Vector3(dx * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad), 0.0f, dz * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
        transform.Rotate(0, x, 0);
        //transform.Rotate(-z, 0, 0);
	}
}
